using Registry.API.Data;
using Registry.API.Repositories.Interfaces;
using Registry.API.ViewModel;

namespace Registry.API.Repositories.implementations;

public class RegistryRepository(RegistryDbContext context) : BaseRepository<Models.Registry>(context) , IRegistryRepository
{
}