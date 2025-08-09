using RentCarServer.Domain.Abstractions;

namespace RentCarServer.Application.Features.Categories;

public sealed class CategoryDto : EntityDto
{
    public string Name { get; set; } = default!;
}
