using GenericRepository;
using RentCarServer.Domain.Users;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Users.DeleteUser;

internal sealed class DeleteUserCommandHandler(IUserRepostiory userRepostiory, IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepostiory.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (user is null)
        {
            return Result<string>.Failure("Kullanıcı bulunamadı.");
        }

        if (user.UserName.Value == "admin")
        {
            return Result<string>.Failure("admin kullanıcısı silinemez.");
        }

        user.Delete();

        userRepostiory.Update(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Kullanıcı başarıyla silindi.";
    }
}
