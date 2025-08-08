using GenericRepository;
using RentCarServer.Application.Services;
using RentCarServer.Domain.Users;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler(IUserRepostiory userRepostiory, IUnitOfWork unitOfWork, IUserContext userContext) : IRequestHandler<UpdateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepostiory.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (user is null)
        {
            return Result<string>.Failure("Kullanıcı bulunamadı.");
        }

        if (user.Email.Value != request.Email)
        {
            var emailExists = await userRepostiory.AnyAsync(x => x.Email.Value == request.Email, cancellationToken);

            if (emailExists)
            {
                return Result<string>.Failure("Bu mail adresi daha önce kullanılmış.");
            }
        }

        if (user.UserName.Value != request.UserName)
        {
            var userNameExists = await userRepostiory.AnyAsync(x => x.UserName.Value == request.UserName, cancellationToken);

            if (userNameExists)
            {
                return Result<string>.Failure("Bu kullanıcı adı daha önce kullanılmış.");
            }
        }

        Guid branchId = request.BranchId is not null ? (Guid)request.BranchId : userContext.GetBranchId();

        user.SetFirstName(new(request.FirstName));
        user.SetLastName(new(request.LastName));
        user.SetEmail(new(request.Email));
        user.SetUserName(new(request.UserName));
        user.SetFullName();
        user.SetBranchId(new(branchId));
        user.SetRoleId(new(request.RoleId));
        user.SetStatus(request.IsActive);

        userRepostiory.Update(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Kullanıcı başarıyla güncellendi.";
    }
}