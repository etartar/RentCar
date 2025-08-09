using RentCarServer.Domain.Abstractions;
using RentCarServer.Domain.Shared;

namespace RentCarServer.Domain.Categories;

public sealed class Category : Entity
{
    private Category()
    {
    }

    public Category(Name name, bool isActive) : base(isActive)
    {
        SetName(name);
        SetStatus(isActive);
    }

    public Name Name { get; private set; } = default!;

    #region Behaviors

    public void SetName(Name name)
    {
        Name = name;
    }

    #endregion
}
