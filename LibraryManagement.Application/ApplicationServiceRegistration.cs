using Amazon.Runtime;
using Amazon.S3;
using FluentValidation;
using LibraryManagement.Application.Abstractions.Services;
using LibraryManagement.Application.Behaviours;
using LibraryManagement.Application.Features.Authentication.Command;
using LibraryManagement.Application.Features.Authentication.Validators;
using LibraryManagement.Application.Features.Books.Commands;
using LibraryManagement.Application.Features.Books.Validators;
using LibraryManagement.Application.Features.LibraryMembers.Commands;
using LibraryManagement.Application.Features.LibraryMembers.Validators;
using LibraryManagement.Application.Features.Members.Commands;
using LibraryManagement.Application.Features.Members.Validators;
using LibraryManagement.Application.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

namespace LibraryManagement.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection ConfigureApplicationService(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IAmazonS3>(sp =>
            {
                var config = new AmazonS3Config
                {
                    ServiceURL = configuration["Cloudflare:ServiceUrl"],
                    ForcePathStyle = true,
                    SignatureVersion = "4",
                    RequestChecksumCalculation = RequestChecksumCalculation.WHEN_REQUIRED,
                    ResponseChecksumValidation = ResponseChecksumValidation.WHEN_REQUIRED
                };

                return new AmazonS3Client(
                    new BasicAWSCredentials(
                        configuration["Cloudflare:AccessKeyId"],
                        configuration["Cloudflare:SecretAccessKey"]
                    ),
                    config
                );
            });

            // Register CloudfareServices
            services.AddScoped<ICloudfareServices, CloudfareServices>();

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(CachingBehaviour<,>));
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddScoped<IValidator<CreateBookCommand>, CreateBookCommandValidator>();
            services.AddScoped<IValidator<UpdateBookCommand>, UpdateBookCommandValidator>();
            services.AddScoped<IValidator<CreateMemberCommand>, CreateMemberCommandValidator>();
            services.AddScoped<IValidator<UpdateMemberCommand>, UpdateMemberCommandValidator>();
            services.AddScoped<IValidator<DeleteMemberCommand>, DeleteMemberCommandValidator>();
            services.AddScoped<IValidator<BorrowBookCommand>, BorrowBookCommandValidator>();
            services.AddScoped<IValidator<ReturnBookCommand>, ReturnBookCommandValidator>();

            services.AddScoped<IValidator<SignUpCommand>, SignUpCommandValidator>();
            services.AddScoped<IValidator<SignInCommand>, SignInCommandValidator>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

            services.AddLogging(b =>
            {
                var logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .CreateLogger();

                b.AddSerilog(logger);
            });

            return services;
        }
    }
}
