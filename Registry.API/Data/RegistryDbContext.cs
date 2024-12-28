using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Registry.API.Models;
using Registry.API.Seeds;

namespace Registry.API.Data;

public class RegistryDbContext(DbContextOptions<RegistryDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Models.Registry> Registries { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
        {
            if (!relationship.IsSelfReferencing())
            {
                relationship.DeleteBehavior = DeleteBehavior.Cascade;
            }
        }
        modelBuilder.AddData(UsersItems.Users);
        modelBuilder.AddData(AuthItems.Roles);
        modelBuilder.AddData(AuthItems.UserRoles);
        modelBuilder.AddData(AuthItems.Permissions);
        modelBuilder.AddData(AuthItems.RolePermissions);
        
        base.OnModelCreating(modelBuilder);
    }
}