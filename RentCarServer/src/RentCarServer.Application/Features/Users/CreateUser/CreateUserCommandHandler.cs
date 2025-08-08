using GenericRepository;
using RentCarServer.Application.Services;
using RentCarServer.Domain.Abstractions;
using RentCarServer.Domain.Users;
using RentCarServer.Domain.Users.ValueObjects;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Users.CreateUser;

internal sealed class CreateUserCommandHandler(IUserRepostiory userRepostiory, IUnitOfWork unitOfWork, IUserContext userContext) : IRequestHandler<CreateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var emailExists = await userRepostiory.AnyAsync(x => x.Email.Value == request.Email, cancellationToken);

        if (emailExists)
        {
            return Result<string>.Failure("Bu mail adresi daha önce kullanılmış.");
        }

        var userNameExists = await userRepostiory.AnyAsync(x => x.UserName.Value == request.UserName, cancellationToken);

        if (userNameExists)
        {
            return Result<string>.Failure("Bu kullanıcı adı daha önce kullanılmış.");
        }

        Guid branchId = request.BranchId is not null ? (Guid)request.BranchId : userContext.GetBranchId();

        FirstName firstName = new(request.FirstName);
        LastName lastName = new(request.LastName);
        Email email = new(request.Email);
        UserName userName = new(request.UserName);
        Password password = new("123");
        IdentityId branchIdRecord = new(branchId);
        IdentityId roleId = new(request.RoleId);


        var user = new User(firstName, lastName, email, userName, password, branchIdRecord, roleId, request.IsActive);

        await userRepostiory.AddAsync(user, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Kullanıcı başarıyla oluşturuldu.";
    }
}