using Microsoft.EntityFrameworkCore;
using Registry.API.Data;
using Registry.API.Models;

namespace Registry.API.Seeds;

public class RegistryDbContextSeeds
{
    public static async Task SeedAsync(RegistryDbContext context, ILogger<RegistryDbContext> logger)
    {
        if (!await context.Users.AnyAsync(x=>x.Id == 1))
        {
            await context.Users.AddRangeAsync(Users());
            await context.SaveChangesAsync();
            logger.LogInformation("data seed section configured");
        }

        if (!await context.Roles.AnyAsync(x=>x.Id == 1))
        {
            await context.Roles.AddRangeAsync(Roles());
            await context.SaveChangesAsync();
            logger.LogInformation("role seed items");
        }
        
        if (!await context.UserRoles.AnyAsync(x=>x.Id == 1))
        {
            await context.UserRoles.AddRangeAsync(UserRoles());
            await context.SaveChangesAsync();
            logger.LogInformation("user roles seed items");
        }
        
        if (!await context.Permissions.AnyAsync(x=>x.Id == 1))
        {
            await context.Permissions.AddRangeAsync(Permissions());
            await context.SaveChangesAsync();
            logger.LogInformation("permissions roles seed items");
        }
        
        if (!await context.RolePermissions.AnyAsync(x=>x.Id == 1))
        {
            await context.Permissions.AddRangeAsync(Permissions());
            await context.SaveChangesAsync();
            logger.LogInformation("permissions roles seed items");
        }
    }

    /// <summary>
    /// user item for seed data
    /// </summary>
    public static IEnumerable<User> Users() => new List<User>()
    {
        new()
        {
            Id = 1,
            Password = "hossein898989",
            ChatId = 5246606864,
            FirstName = "Admin",
            LastName = "Adminy",
            UserName = "Hossein FARAJI",
            IsDelete = false,
            CreateBy = 1,
            ModifyBy = 1,
            ModifiedDate = DateTime.UtcNow,
            CreateDate = DateTime.UtcNow,
        }
    };

    public static List<Role> Roles()
    {
        return new()
        {
            new()
            {
                Id = 1,
                Title = "Supporter",
                CreateDate = DateTime.UtcNow,
                IsDelete = false,
                ModifiedDate = DateTime.UtcNow,
                CreateBy = 1,
                ModifyBy = 1,
            }
        };
    }

    public static List<UserRole> UserRoles()
    {
        return new()
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
    }

    public static List<Permission> Permissions()
    {
        return new()
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
    }

    public static List<RolePermission> RolePermissions()
    {
        return new()
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
}