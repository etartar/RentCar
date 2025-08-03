﻿using RentCarServer.Domain.Abstractions;
using RentCarServer.Domain.Shared;

namespace RentCarServer.Domain.Roles;

public sealed class Role : Entity, IAggregate
{
    private readonly List<Permission> _permissions = new();

    private Role()
    {
    }

    public Role(Name name, bool isActive) : base(isActive)
    {
        SetName(name);
        SetStatus(isActive);
    }

    public Name Name { get; private set; } = default!;

    public IReadOnlyCollection<Permission> Permissions => _permissions;

    #region Behaviors

    public void SetName(Name name)
    {
        Name = name;
    }

    public void SetPermissions(IEnumerable<Permission> permissions)
    {
        _permissions.Clear();
        _permissions.AddRange(permissions);
    }

    #endregion
}


public sealed record Permission(string Value);