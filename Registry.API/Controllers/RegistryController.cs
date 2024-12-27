using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Registry.API.Extensions;
using Registry.API.Filters;
using Registry.API.Services;
using Registry.API.ViewModel;

namespace Registry.API.Controllers;

/// <summary>
/// Controller for managing registry operations such as adding and retrieving registries.
/// Inherits from <see cref="DefaultController"/>.
/// </summary>
public class RegistryController(IRegistryService service) : DefaultController
{
    /// <summary>
    /// Handles the addition of a new registry entry.
    /// Validates that the IMEI values are unique before saving the registry.
    /// </summary>
    /// <param name="registry">The registry data to add, provided as an <see cref="AddRegistryDto"/> object.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
    /// <exception cref="ValidationException">Thrown if the IMEI values already exist in the database.</exception>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResult),(int)HttpStatusCode.OK)]
    public async Task<ApiResult> Post([FromBody] AddRegistryDto registry)
    {
        await service.AddAsync(registry,User.GetId());
        return Ok();
    }
    
    /// <summary>
    /// Retrieves a filtered list of registry entries.
    /// If no filter is provided, returns all registry entries.
    /// </summary>
    /// <param name="filter">The filtering criteria as a <see cref="FilterRegistryDto"/> object.</param>
    /// <returns>An <see cref="IActionResult"/> containing the filtered list of registry entries.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<FilterRegistryDto>),(int)HttpStatusCode.OK)]
    public async Task<ApiResult<FilterRegistryDto>> Get([FromQuery] FilterRegistryDto filter)
    {
        return Ok(await service.FilterAsync(filter,User.GetId()));
    }

    /// <summary>
    /// Updates the registry entry by accepting the provided registry data.
    /// </summary>
    /// <param name="accept">The registry data to accept, provided as an <see cref="AcceptRegistryDto"/> object.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the update operation, wrapped in an <see cref="ApiResult"/>.</returns>
    /// <response code="200">Returns an OK status with the updated registry data.</response>
    [HttpPut]
    [ProducesResponseType(typeof(ApiResult<FilterRegistryDto>),(int)HttpStatusCode.OK)]
    public async Task<ApiResult> Update([FromBody] AcceptRegistryDto accept)
    {
        await service.AcceptRegistryAsync(accept); 
        return Ok();
    }
}