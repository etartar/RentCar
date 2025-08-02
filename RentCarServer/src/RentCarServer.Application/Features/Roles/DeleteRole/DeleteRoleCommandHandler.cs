using GenericRepository;
using RentCarServer.Domain.Roles;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Roles.DeleteRole;

internal sealed class DeleteRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (role is null)
        {
            return Result<string>.Failure("Rol bulunamadı.");
        }

        role.Delete();

        roleRepository.Update(role);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Rol başarılı bir şekilde silindi.";
    }
}
