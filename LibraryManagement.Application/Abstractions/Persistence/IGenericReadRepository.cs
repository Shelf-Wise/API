namespace LibraryManagement.Application.Abstractions.Persistence
{
    public interface IGenericReadRepository<T>
        where T : class
    {
        Task<T?> GetByIdAsync(Guid id, string tableName, string schemaName = "public");
        Task<IReadOnlyCollection<T>> GetAllAsync(string tableName, string schemaName = "public");
        Task<bool> EntityExists(Guid id, string tableName, string schemaName = "public");
    }
}
