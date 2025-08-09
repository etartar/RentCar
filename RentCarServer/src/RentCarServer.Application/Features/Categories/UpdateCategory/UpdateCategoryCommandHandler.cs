using GenericRepository;
using RentCarServer.Domain.Categories;
using RentCarServer.Domain.Shared;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Categories.UpdateCategory;

public sealed class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (category is null)
        {
            return Result<string>.Failure("Kategori bulunamadı.");
        }

        if (category.Name.Value != request.Name)
        {
            var nameExists = await categoryRepository.AnyAsync(x => x.Name.Value == request.Name, cancellationToken);

            if (nameExists)
            {
                return Result<string>.Failure("Bu kategori adı daha önce tanımlanmış.");
            }
        }

        category.SetName(new Name(request.Name));
        category.SetStatus(request.IsActive);

        categoryRepository.Update(category);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Kategori bilgisi başarılı şekilde güncellendi.";
    }
}

