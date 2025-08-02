using GenericRepository;
using RentCarServer.Domain.Roles;
using RentCarServer.Domain.Shared;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Roles.CreateRole;

public sealed class CreateRoleCommandHandler(
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var nameIsExists = await roleRepository.AnyAsync(b => b.Name.Value == request.Name, cancellationToken);

        if (nameIsExists)
        {
            return Result<string>.Failure($"Bu rol adı '{request.Name}' sistemde kayıtlıdır.");
        }

        Name name = new Name(request.Name);

        Role role = new Role(name, request.IsActive);

        await roleRepository.AddAsync(role, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Rol başarılı şekilde kayıt edildi.";
    }
}