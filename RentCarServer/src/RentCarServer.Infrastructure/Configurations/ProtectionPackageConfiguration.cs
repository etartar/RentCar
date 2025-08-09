﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentCarServer.Domain.ProtectionPackages;

namespace RentCarServer.Infrastructure.Configurations;

internal sealed class ProtectionPackageConfiguration : IEntityTypeConfiguration<ProtectionPackage>
{
    public void Configure(EntityTypeBuilder<ProtectionPackage> builder)
    {
        builder.ToTable("ProtectionPackages");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Name);

        builder.OwnsOne(x => x.Price);

        builder.OwnsOne(x => x.IsRecommended);

        builder.OwnsOne(x => x.OrderNumber);

        builder.OwnsMany(x => x.Coverages, permissions =>
        {
            permissions.ToTable("ProtectionPackageCoverages");
        });
    }
}
