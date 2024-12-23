using Microsoft.EntityFrameworkCore;
using Registry.API.Models;

namespace Registry.API.Data;

public class RegistryDbContext(DbContextOptions<RegistryDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Models.Registry> Registries { get; set; }
}