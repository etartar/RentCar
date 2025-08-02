using GenericRepository;
using Microsoft.EntityFrameworkCore;
using RentCarServer.Domain.Abstractions;
using RentCarServer.Domain.Users;

namespace RentCarServer.Infrastructure.Abstractions;

internal class AuditableRepository<TEntity, TContext> : Repository<TEntity, TContext>, IAuditableRepository<TEntity>
    where TEntity : Entity
    where TContext : DbContext
{
    private readonly TContext _dbContext;

    public AuditableRepository(TContext context) : base(context)
    {
        _dbContext = context;
    }

    public IQueryable<EntityWithAuditDto<TEntity>> GetAllWithAudit()
    {
        var entities = _dbContext.Set<TEntity>().AsNoTracking().AsQueryable();
        var users = _dbContext.Set<User>().AsNoTracking().AsQueryable();

        var res = entities
            .Join(users, m => m.CreatedBy, m => m.Id, (e, user) => new
            {
                entity = e,
                createdUser = user
            })
            .GroupJoin(users, m => m.entity.UpdatedBy, m => m.Id, (e, user) => new
            {
                e.entity,
                e.createdUser,
                updatedUser = user
            })
            .SelectMany(s => s.updatedUser.DefaultIfEmpty(), (x, updatedUser) => new EntityWithAuditDto<TEntity>
            {
                Entity = x.entity,
                CreatedUser = x.createdUser,
                UpdatedUser = updatedUser
            });

        return res;
    }
}
