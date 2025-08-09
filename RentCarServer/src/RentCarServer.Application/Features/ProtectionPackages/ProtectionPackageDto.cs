using RentCarServer.Domain.Abstractions;

namespace RentCarServer.Application.Features.ProtectionPackages;

public sealed class ProtectionPackageDto : EntityDto
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public bool IsRecommended { get; set; }
    public int CoverageCount { get; set; }
    public int OrderNumber { get; set; }
    public List<string> Coverages { get; set; } = new List<string>();
}
