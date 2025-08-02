using GenericRepository;

namespace RentCarServer.Domain.Abstractions;

public interface IAuditableRepository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    IQueryable<EntityWithAuditDto<TEntity>> GetAllWithAudit();
}
