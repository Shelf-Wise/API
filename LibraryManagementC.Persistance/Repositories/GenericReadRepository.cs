using System.Data;
using Dapper;
using LibraryManagement.Application.Abstractions.Persistence;

namespace LibraryManagementC.Persistance.Repositories
{
    public class GenericReadRepository<T> : IGenericReadRepository<T>
        where T : class
    {
        protected readonly IDbConnection _connection;

        // used because of RawSQL, (Dapper) <-- an ORM used for lightweight reads and RawSQL Reads

        public GenericReadRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(
            string tableName,
            string schemaName = "public"
        )
        {
            var query = $"SELECT * FROM \"{schemaName}\".\"{tableName}\"";
            var entities = await _connection.QueryAsync<T>(query);

            return entities.AsList().AsReadOnly();
        }

        public async Task<T?> GetByIdAsync(Guid id, string tableName, string schemaName = "public")
        {
            string query = $"SELECT * FROM \"{schemaName}\".\"{tableName}\" WHERE \"Id\" = @Id";
            var data = await _connection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
            return data;
        }

        public async Task<bool> EntityExists(Guid id, string tableName, string schemaName = "public")
        {
            var query = $"SELECT 1 FROM \"{schemaName}\".\"{tableName}\" WHERE Id = @Id";
            var data = await _connection.ExecuteScalarAsync<Guid?>(query);
            return data.HasValue;
        }

        //public async Task<LibraryMember?> GetByLibraryMembers(string tableName, string schemaName = "dbo")
        //{
        //    var LibMember = (int)MemberType.LiraryMember;
        //    var query = $"SELECT * FROM \"{schemaName}\".\"{tableName}\" WHERE MemberType = @LibMember";
        //    var data = await _connection.QueryAsync<LibraryMember>(query, new { LibMember });
        //    return data
        //}
    }
}
