using LibraryManagement.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryManagementC.Persistance.Repositories
{
    public class GenericWriteRepository<T> : IGenericWriteRepository<T>
        where T : class
    {
        protected readonly ApplicationWriteDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericWriteRepository(ApplicationWriteDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T> AddEntityAsync(T entity)
        {
            var done = await _dbSet.AddAsync(entity);

            return entity;
        }

        // _dbContext.Set<TEntity>(): This retrieves the DbSet<TEntity>, which allows operations like querying, adding, updating, or removing entities of type TEntity.
        public virtual bool DeleteEntityAsync(T entity)
        {
            _context.Remove(entity);
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var val = await _dbSet.FindAsync(id);
            return val != null;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual bool UpdateEntityAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
            return true;
        }

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> Get(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> Get(Guid id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithInclude(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<List<T>> Find(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AsNoTracking().Where(expression).ToListAsync();
        }

        public async Task<List<TResult>> Find<TResult>(Expression<Func<T, bool>> expression, Expression<Func<T, TResult>> selector)
        {
            return await _context.Set<T>().Where(expression).Select(selector).ToListAsync();
        }

        public async Task<List<T>> FindWithInclude(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IQueryable<T>>? includeExpression = null)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            if (includeExpression != null)
            {
                query = includeExpression(query);
            }

            return await query.Where(expression).ToListAsync();
        }

        public async Task<T> GetWithInclude(Guid id, params string[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        public async Task ExecuteSqlRaw(string sql, params object[] parameters)
        {
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }


    }
}
