using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentCarServer.Domain.Branches;

namespace RentCarServer.Infrastructure.Configurations;

internal sealed class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.HasKey(b => b.Id);

        builder.OwnsOne(b => b.Name);

        builder.OwnsOne(b => b.Address);

        builder.OwnsOne(b => b.Contact);

        /*builder.OwnsOne(b => b.Address, address =>
        {
            address.Property(a => a.City).HasMaxLength(50).IsRequired();
            address.Property(a => a.District).HasMaxLength(50).IsRequired();
            address.Property(a => a.FullAddress).HasMaxLength(20).IsRequired();
        });*/
    }
}
