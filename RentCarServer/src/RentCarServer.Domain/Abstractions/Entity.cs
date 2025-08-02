namespace RentCarServer.Domain.Abstractions;

public abstract class Entity
{
    protected Entity()
    {
        Id = new IdentityId(Guid.CreateVersion7());
        IsActive = true;
    }

    protected Entity(bool isActive)
    {
        Id = new IdentityId(Guid.CreateVersion7());
        IsActive = isActive;
    }

    public IdentityId Id { get; private set; }
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public IdentityId CreatedBy { get; private set; } = default!;
    public DateTimeOffset? UpdatedAt { get; private set; }
    public IdentityId? UpdatedBy { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public IdentityId? DeletedBy { get; private set; }

    public void SetStatus(bool isActive)
    {
        IsActive = isActive;
    }

    //public void SetCreatedBy()
    //{
    //    CreatedBy = new IdentityId(Guid.CreateVersion7());
    //}

    public void Delete()
    {
        IsDeleted = true;
    }
}
