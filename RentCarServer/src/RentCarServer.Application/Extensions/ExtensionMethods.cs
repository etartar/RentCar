using RentCarServer.Domain.Abstractions;
using RentCarServer.Domain.Users;

namespace RentCarServer.Application.Extensions;

internal static class ExtensionMethods
{
    public static IQueryable<EntityWithAuditDto<TEntity>> ApplyAuditDto<TEntity>(
        this IQueryable<TEntity> entities,
        IQueryable<User> users) where TEntity : Entity
    {
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
