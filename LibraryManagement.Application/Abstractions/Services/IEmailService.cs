using LibraryManagementC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Abstractions.Services
{
    public interface IEmailService
    {
        Task SendBookBorrowedEmailAsync(Member member, Book book, DateTime dueDate);
        Task SendBookReturnedEmailAsync(Member member, Book book, DateTime returnDate, decimal fine = 0);

    }
}
