using Microsoft.EntityFrameworkCore;
using Registry.API.Models;

namespace Registry.API.Data;

public class RegistryDbContext(DbContextOptions<RegistryDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Models.Registry> Registries { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
}