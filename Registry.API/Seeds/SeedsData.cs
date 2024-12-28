using Microsoft.EntityFrameworkCore;
using Registry.API.Common;
using Registry.API.Models;

namespace Registry.API.Seeds;

public static class SeedsData
{
    public static void AddData<TEntity>(this ModelBuilder model, List<TEntity> data) where TEntity : EntityBase
    {
        model.Entity<TEntity>().HasData(data);
    }
}

/// <summary>
/// user item for seed data
/// </summary>
public abstract class UsersItems
{
    public static List<User> Users = new()
    {
        new()
        {
            Id = 1,
            Password = "hossein898989",
            ChatId = 5246606864,
            FirstName = "Admin",
            LastName = "Adminy",
            UserName = "Hossein FARAJI",
            CreateDate = DateTime.UtcNow,
            IsDelete = false,
        }
    };
}


/// <summary>
/// authentication and authorization items
/// </summary>
public abstract class AuthItems
{
    public static List<Role> Roles = new()
    {
        new()
        {
            Id = 1,
            Title = "Supporter",
            CreateDate = DateTime.UtcNow,
            IsDelete = false,
        }
    };

    public static List<UserRole> UserRoles = new()
    {
        new()
        {
            Id = 1,
            UserId = 1,
            RoleId = 1,
            CreateDate = DateTime.UtcNow,
            IsDelete = false,
        }
    };

    public static List<Permission> Permissions = new()
    {
        new()
        {
            Id = 1,
            Title = "supporter",
            SystemName = "supporter",
            CreateDate = DateTime.UtcNow,
            IsDelete = false,
        }
    };

    public static List<RolePermission> RolePermissions = new()
    {
        new()
        {
            Id = 1,
            PermissionId = 1,
            RoleId = 1,
            CreateDate = DateTime.UtcNow,
            IsDelete = false,
        }
    };
}