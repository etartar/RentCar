using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentCarServer.Domain.Users;

namespace RentCarServer.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.FirstName);

        builder.OwnsOne(x => x.LastName);

        builder.OwnsOne(x => x.FullName);

        builder.OwnsOne(x => x.Email);

        builder.OwnsOne(x => x.UserName);

        builder.OwnsOne(x => x.Password);

        builder.OwnsOne(x => x.ForgotPasswordCode);

        builder.OwnsOne(x => x.ForgotPasswordCreatedDate);

        builder.OwnsOne(x => x.IsForgotPasswordCompleted);

        builder.OwnsOne(x => x.TFAStatus);

        builder.OwnsOne(x => x.TFACode);

        builder.OwnsOne(x => x.TFAConfirmCode);

        builder.OwnsOne(x => x.TFAExpiresDate);

        builder.OwnsOne(x => x.TFAIsCompleted);
    }
}
