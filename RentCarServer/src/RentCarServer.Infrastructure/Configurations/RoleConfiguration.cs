using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentCarServer.Domain.Roles;

namespace RentCarServer.Infrastructure.Configurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Name);

        builder.OwnsMany(x => x.Permissions, permissions =>
        {
            permissions.ToTable("Permissions");
        });

        //builder.OwnsMany(x => x.Permissions, permissions =>
        //{
        //    permissions.WithOwner().HasForeignKey("RoleId");
        //    permissions.Property(p => p.Value).HasColumnName("Permission").IsRequired();
        //    permissions.HasKey("RoleId", "Value");
        //});
    }
}
