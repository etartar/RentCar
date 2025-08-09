using RentCarServer.Domain.Abstractions;

namespace RentCarServer.Application.Features.Extras;

public class ExtraDto : EntityDto
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public string Description { get; set; } = default!;
}
