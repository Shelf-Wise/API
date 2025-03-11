using System.Linq.Expressions;

namespace LibraryManagement.Application.Abstractions.Persistence
{
    public interface IGenericWriteRepository<T>
        where T : class
    {
        Task<T> AddEntityAsync(T entity);
        bool UpdateEntityAsync(T entity);
        bool DeleteEntityAsync(T entity);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id);
        Task<T> Get(int id);
        Task<T> Get(Guid id);
        Task<T> Get(Guid id, params Expression<Func<T, object>>[] includes);
        Task<IReadOnlyList<T>> GetAll();
        Task<IReadOnlyList<T>> GetAllWithInclude(params Expression<Func<T, object>>[] includes);
        Task<List<T>> Find(Expression<Func<T, bool>> expression);
        Task<List<TResult>> Find<TResult>(Expression<Func<T, bool>> expression, Expression<Func<T, TResult>> selector);
        Task<List<T>> FindWithInclude(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IQueryable<T>>? includeExpression = null);







    }
}
