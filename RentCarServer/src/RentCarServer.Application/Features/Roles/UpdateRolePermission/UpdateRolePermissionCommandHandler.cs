using GenericRepository;
using RentCarServer.Domain.Roles;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Roles.UpdateRolePermission;

internal sealed class UpdateRolePermissionCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateRolePermissionCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.FirstOrDefaultAsync(x => x.Id == request.RoleId, cancellationToken);

        if (role is null)
        {
            return Result<string>.Failure("Rol bulunamadı.");
        }

        List<Permission> permissions = request.Permissions.Select(p => new Permission(p)).ToList();

        role.SetPermissions(permissions);

        roleRepository.Update(role);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("İşlem başarıyla tamamlandı");
    }
}