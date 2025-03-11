using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagementC.Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LibraryManagementC.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationWriteDbContext _context;
        private readonly IMediator _mediator;

        public UnitOfWork(ApplicationWriteDbContext applicationWriteDbContext, IMediator mediator)
        {
            _context = applicationWriteDbContext;
            _mediator = mediator;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditableEntities();
            await _context.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditableEntities()
        {
            IEnumerable<EntityEntry<BaseDomainAuditableEntity>> entries =
                _context.ChangeTracker.Entries<BaseDomainAuditableEntity>();

            foreach (EntityEntry<BaseDomainAuditableEntity> entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Entity.CreatedAt = DateTime.UtcNow;
                    entityEntry.Entity.UpdateAt = DateTime.UtcNow;
                }

                if (entityEntry.State == EntityState.Modified)
                    entityEntry.Entity.CreatedBy = entityEntry.Entity.CreatedBy;
                    entityEntry.Entity.UpdateAt = DateTime.UtcNow;
            }
        }
    }
}
