using LibraryManagement.Application.Abstractions.Services;
using LibraryManagementC.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace LibraryManagement.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;
        private readonly string _senderEmail;
        private readonly string _senderName;
        private readonly bool _emailEnabled;

        public EmailService(
            ISendGridClient sendGridClient,
            IConfiguration configuration,
            ILogger<EmailService> logger)
        {
            _sendGridClient = sendGridClient ?? throw new ArgumentNullException(nameof(sendGridClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _senderEmail = _configuration["EmailSettings:SenderEmail"];
            _senderName = _configuration["EmailSettings:SenderName"];
            _emailEnabled = _configuration.GetValue<bool>("EmailSettings:Enabled", false);

            if (_emailEnabled && (string.IsNullOrEmpty(_senderEmail) || string.IsNullOrEmpty(_senderName)))
            {
                throw new InvalidOperationException("Sender email or name is not configured properly.");
            }
        }

        public async Task SendBookBorrowedEmailAsync(Member member, Book book, DateTime dueDate)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));
            if (book == null) throw new ArgumentNullException(nameof(book));

            if (!_emailEnabled)
            {
                _logger.LogInformation("Email service is disabled. Skipping book borrowed email for member {MemberId}", member.Id);
                return;
            }

            try
            {
                var subject = "Book Borrowed Confirmation";
                var templateId = _configuration["EmailSettings:Templates:BookBorrowed"];

                if (string.IsNullOrEmpty(templateId))
                {
                    _logger.LogWarning("Book borrowed template ID is not configured. Skipping email.");
                    return;
                }

                var dynamicTemplateData = new
                {
                    memberName = $"{member.FullName}",
                    bookTitle = book.Title,
                    author = book.Author,
                    borrowDate = DateTime.Now.ToString("MMMM dd, yyyy"),
                    dueDate = dueDate.ToString("MMMM dd, yyyy"),
                    libraryName = _configuration["LibrarySettings:Name"] ?? "Our Library"
                };

                await SendTemplatedEmailAsync(member.Email, subject, templateId, dynamicTemplateData);
                _logger.LogInformation("Book borrowed email sent successfully to {Email} for book {BookId}", member.Email, book.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send book borrowed email to {Email} for book {BookId}", member.Email, book.Id);

                // Rethrow if configured to do so, otherwise just log the error
                if (_configuration.GetValue<bool>("EmailSettings:FailOnError", false))
                {
                    throw;
                }
            }
        }

        public async Task SendBookReturnedEmailAsync(Member member, Book book, DateTime returnDate, decimal fine = 0)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));
            if (book == null) throw new ArgumentNullException(nameof(book));

            if (!_emailEnabled)
            {
                _logger.LogInformation("Email service is disabled. Skipping book returned email for member {MemberId}", member.Id);
                return;
            }

            try
            {
                var subject = "Book Return Confirmation";
                var templateId = _configuration["EmailSettings:Templates:BookReturned"];

                if (string.IsNullOrEmpty(templateId))
                {
                    _logger.LogWarning("Book returned template ID is not configured. Skipping email.");
                    return;
                }

                var dynamicTemplateData = new
                {
                    memberName = $"{member.FullName}",
                    bookTitle = book.Title,
                    author = book.Author,
                    returnDate = returnDate.ToString("MMMM dd, yyyy"),
                    fine = fine.ToString("C"),
                    hasFine = fine > 0,
                    libraryName = _configuration["LibrarySettings:Name"] ?? "Our Library"
                };

                await SendTemplatedEmailAsync(member.Email, subject, templateId, dynamicTemplateData);
                _logger.LogInformation("Book returned email sent successfully to {Email} for book {BookId}", member.Email, book.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send book returned email to {Email} for book {BookId}", member.Email, book.Id);

                // Rethrow if configured to do so, otherwise just log the error
                if (_configuration.GetValue<bool>("EmailSettings:FailOnError", false))
                {
                    throw;
                }
            }
        }

        private async Task SendTemplatedEmailAsync(string recipientEmail, string subject, string templateId, object templateData)
        {
            if (string.IsNullOrEmpty(recipientEmail)) throw new ArgumentException("Recipient email cannot be null or empty", nameof(recipientEmail));
            if (string.IsNullOrEmpty(templateId)) throw new ArgumentException("Template ID cannot be null or empty", nameof(templateId));

            var from = new EmailAddress(_senderEmail, _senderName);
            var to = new EmailAddress(recipientEmail);

            var message = MailHelper.CreateSingleTemplateEmail(from, to, templateId, templateData);
            message.SetSubject(subject);

            _logger.LogDebug("Attempting to send email to {Email} using template {TemplateId}", recipientEmail, templateId);

            var response = await _sendGridClient.SendEmailAsync(message);

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                var responseBody = await response.Body.ReadAsStringAsync();
                _logger.LogError("SendGrid returned Forbidden (403) status code. API Key may be invalid or lacks permissions. Response: {Response}", responseBody);
                throw new Exception($"SendGrid API access forbidden. Check API key and permissions: {responseBody}");
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _logger.LogError("SendGrid returned Unauthorized (401) status code. API Key is invalid.");
                throw new Exception("SendGrid authentication failed. Invalid API key.");
            }
            else if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.Body.ReadAsStringAsync();
                _logger.LogError("Failed to send email. Status code: {StatusCode}. Response: {Response}", response.StatusCode, responseBody);
                throw new Exception($"Failed to send email. Status code: {response.StatusCode}. Response: {responseBody}");
            }

            _logger.LogDebug("Email sent successfully to {Email}", recipientEmail);
        }
    }
}