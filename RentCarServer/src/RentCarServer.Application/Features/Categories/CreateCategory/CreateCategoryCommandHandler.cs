using GenericRepository;
using RentCarServer.Domain.Categories;
using RentCarServer.Domain.Shared;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Categories.CreateCategory;

public sealed class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var nameIsExists = await categoryRepository.AnyAsync(b => b.Name.Value == request.Name, cancellationToken);

        if (nameIsExists)
        {
            return Result<string>.Failure($"Bu kategori adı '{request.Name}' sistemde kayıtlıdır.");
        }

        var category = new Category(new Name(request.Name), request.IsActive);

        await categoryRepository.AddAsync(category, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Kategori başarılı şekilde kayıt edildi.";
    }
}
