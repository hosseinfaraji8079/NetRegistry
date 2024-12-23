using Registry.API.ViewModel;

namespace Registry.API.Repositories.Interfaces;

/// <summary>
/// Repository interface for handling registry-related data operations.
/// Extends the generic asynchronous repository interface for <see cref="Models.Registry"/>.
/// </summary>
public interface IRegistryRepository : IAsyncRepository<Models.Registry>
{

}
