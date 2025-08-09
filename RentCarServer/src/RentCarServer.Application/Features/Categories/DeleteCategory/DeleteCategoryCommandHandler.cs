using GenericRepository;
using RentCarServer.Domain.Categories;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Categories.DeleteCategory;

internal sealed class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (category is null)
        {
            return Result<string>.Failure("Kategori bulunamadý.");
        }

        category.Delete();

        categoryRepository.Update(category);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Kategori baþarýlý bir þekilde silindi.";
    }
}
