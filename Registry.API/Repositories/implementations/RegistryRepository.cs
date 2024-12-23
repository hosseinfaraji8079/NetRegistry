using Registry.API.Data;
using Registry.API.Repositories.Interfaces;

namespace Registry.API.Repositories.implementations;

public class RegistryRepository(RegistryDbContext context) : BaseRepository<Models.Registry>(context) , IRegistryRepository
{
    
}