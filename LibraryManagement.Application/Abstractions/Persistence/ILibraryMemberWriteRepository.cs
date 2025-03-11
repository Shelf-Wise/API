using LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Abstractions.Persistence
{
    public interface ILibraryMemberWriteRepository : IGenericWriteRepository<Librarian>
    {
        Task<bool> BorrowBookAsync(Librarian Member, Book book);
    }
}
