using System.Linq.Expressions;
using Registry.API.Models;
using Registry.API.Repositories.Interfaces;

namespace Registry.API.Services;

public class AuthorizationService(IUserRoleRepository userRoleRepository) : IAuthorizationService
{
    public async Task<bool> HasUserPermission(long userId, string permissionName)
    {
        var userRoles = await userRoleRepository.GetAsync(
            ur => ur.UserId == userId && !ur.IsDelete,
            includeString: "Role.RolePermissions.Permission"
        );

        return userRoles.Any(userRole =>
            userRole.Role.RolePermissions.Any(rp =>
                rp.Permission != null && rp.Permission.SystemName == permissionName));
    }

    public async Task<List<string>> GetUserPermissions(long userId)
    {
        var userRoles = await userRoleRepository.GetAsync(
            ur => ur.UserId == userId && !ur.IsDelete,
            includeString: "Role.RolePermissions.Permission"
        );

        var permissions = userRoles
            .SelectMany(userRole => userRole.Role.RolePermissions)
            .Where(rp => rp.Permission != null)
            .Select(rp => rp.Permission.SystemName)
            .Distinct();

        return permissions.ToList();
    }
}