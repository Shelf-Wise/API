using LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Abstractions.Persistence
{
    public interface ILibraryMemberReadRepository
    {
        Task<Librarian?> GetLibraryMemberByIdAsync(Guid id);
        Task<IReadOnlyCollection<Librarian>> GetAllLibraryMembersAsync();
    }
}
