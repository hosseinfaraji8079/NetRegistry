using Registry.API.Data;
using Registry.API.Models;
using Registry.API.Repositories.Interfaces;

namespace Registry.API.Repositories.implementations;

public class UserRoleRepository(RegistryDbContext context)
    : BaseRepository<UserRole>(context), IUserRoleRepository;