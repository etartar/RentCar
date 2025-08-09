using GenericRepository;
using Microsoft.EntityFrameworkCore;
using RentCarServer.Application.Services;
using RentCarServer.Domain.Abstractions;
using RentCarServer.Domain.Branches;
using RentCarServer.Domain.Categories;
using RentCarServer.Domain.LoginTokens;
using RentCarServer.Domain.ProtectionPackages;
using RentCarServer.Domain.Roles;
using RentCarServer.Domain.Users;
using RentCarServer.Infrastructure.Extensions;
using RentCarServer.Infrastructure.ValueConverters;

namespace RentCarServer.Infrastructure.Context;

internal sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IUserContext _userContext;

    public ApplicationDbContext(DbContextOptions options, IUserContext userContext) : base(options)
    {
        _userContext = userContext;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<LoginToken> LoginTokens { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProtectionPackage> ProtectionPackages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.ApplyGlobalFilters();

        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<IdentityId>().HaveConversion<IdentityIdValueConverter>();
        configurationBuilder.Properties<decimal>().HaveColumnType("money");
        configurationBuilder.Properties<string>().HaveColumnType("nvarchar(MAX)");

        base.ConfigureConventions(configurationBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<Entity>();

        Guid? userId = _userContext.GetUserId();

        if (userId is null)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        IdentityId identityId = new(userId.Value);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(p => p.CreatedAt)
                    .CurrentValue = DateTimeOffset.Now;
                entry.Property(p => p.CreatedBy)
                    .CurrentValue = identityId;
            }

            if (entry.State == EntityState.Modified)
            {
                if (entry.Property(p => p.IsDeleted).CurrentValue == true)
                {
                    entry.Property(p => p.DeletedAt)
                    .CurrentValue = DateTimeOffset.Now;
                    entry.Property(p => p.DeletedBy)
                    .CurrentValue = identityId;
                }
                else
                {
                    entry.Property(p => p.UpdatedAt)
                        .CurrentValue = DateTimeOffset.Now;
                    entry.Property(p => p.UpdatedBy)
                    .CurrentValue = identityId;
                }
            }

            if (entry.State == EntityState.Deleted)
            {
                throw new ArgumentException("Db'den direkt silme işlemi yapamazsınız");
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
