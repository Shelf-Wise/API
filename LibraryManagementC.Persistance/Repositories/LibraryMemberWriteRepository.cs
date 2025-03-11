using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagementC.Domain.Entities;

namespace LibraryManagementC.Persistance.Repositories
{
    public class LibraryMemberWriteRepository
        : GenericWriteRepository<Librarian>,
            ILibraryMemberWriteRepository
    {
        public LibraryMemberWriteRepository(ApplicationWriteDbContext context)
            : base(context) { }

        public new Task<Librarian> AddEntityAsync(Librarian entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BorrowBookAsync(Librarian Member, Book book)
        {
            throw new NotImplementedException();
        }

        public new bool DeleteEntityAsync(Librarian entity)
        {
            throw new NotImplementedException();
        }

        public new Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public new Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public new bool UpdateEntityAsync(Librarian entity)
        {
            throw new NotImplementedException();
        }
    }
}
