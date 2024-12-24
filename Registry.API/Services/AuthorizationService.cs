using System.Linq.Expressions;
using Registry.API.Models;
using Registry.API.Repositories.Interfaces;

namespace Registry.API.Services;

public class AuthorizationService(IUserRoleRepository userRoleRepository) : IAuthorizationService
{
    public async Task<bool> HasUserPermission(long userId, string permissionName)
    {
        return await userRoleRepository.GetAsync(
            userRole => userRole.UserId == userId && !userRole.IsDelete,
            includes: new List<Expression<Func<UserRole, object>>>
            {
                ur => ur.Role!,
                ur => ur.Role.RolePermissions,
                ur => ur.Role.RolePermissions.Select(rp => rp.Permission)
            }
        ).ContinueWith(task => task.Result.Any(userRole =>
            userRole.Role.RolePermissions.Any(rolePermission =>
                rolePermission.Permission.SystemName == permissionName)));
    }

}