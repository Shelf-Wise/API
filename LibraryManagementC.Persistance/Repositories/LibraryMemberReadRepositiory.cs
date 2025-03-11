using System.Data;
using Dapper;
using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagementC.Domain.Entities;

namespace LibraryManagementC.Persistance.Repositories
{
    public class LibraryMemberReadRepositiory : ILibraryMemberReadRepository
    {
        private readonly IDbConnection _connection;

        public LibraryMemberReadRepositiory(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IReadOnlyCollection<Librarian>> GetAllLibraryMembersAsync()
        {
            var query = $"SELECT * FROM Librarian";
            var entities = await _connection.QueryAsync<Librarian>(query);

            return entities.AsList().AsReadOnly();
        }

        public async Task<Librarian?> GetLibraryMemberByIdAsync(Guid id)
        {
            var query = $"SELECT * FROM Members WHERE MemberTypeLib2 = 1 AND Id = @Id";
            var entity = await _connection.QueryFirstOrDefaultAsync<Librarian>(
                query,  
                new { Id = id }
            );

            return entity;
        }
    }
}
