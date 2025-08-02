using GenericRepository;
using RentCarServer.Domain.Roles;
using RentCarServer.Domain.Shared;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Roles.UpdateRole;

public sealed class UpdateRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (role is null)
        {
            return Result<string>.Failure("Rol bulunamadı.");
        }

        Name name = new Name(request.Name);

        role.SetName(name);
        role.SetStatus(request.IsActive);

        roleRepository.Update(role);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Rol bilgisi başarılı şekilde güncellendi.";
    }
}
