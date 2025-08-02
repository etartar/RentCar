using RentCarServer.Application.Attributes;
using RentCarServer.Application.Services;
using System.Reflection;
using TS.MediatR;

namespace RentCarServer.Application.Behaviors;

internal sealed class PermissionBehavior<TRequest, TResponse>(
    IUserContext userContext) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken = default)
    {
        var attr = request.GetType().GetCustomAttribute<PermissionAttribute>(inherit: true);

        if (attr is null) return await next();

        Guid? userId = userContext.GetUserId();
        //var user = await userRepository.FirstOrDefaultAsync(p => p.Id == userId, cancellationToken);
        //if (user is null)
        //{
        //    throw new ArgumentException("User bulunamadı");
        //}

        //// Eğer permission string'i varsa kontrol et
        //if (!string.IsNullOrEmpty(attr.Permission))
        //{
        //    var hasPermission = user.Permissions.Any(p => p.Name == attr.Permission);
        //    if (!hasPermission)
        //    {
        //        throw new AuthorizationException($"'{attr.Permission}' yetkisine sahip değilsiniz.");
        //    }
        //}

        //// Eğer permission string'i yoksa sadece admin kontrolü yap
        //else if (!user.IsAdmin.Value)
        //{
        //    throw new AuthorizationException("Bu işlem için admin yetkisi gereklidir.");
        //}

        return await next();
    }
}
