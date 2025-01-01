using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Registry.API.Extensions;
using Registry.API.Filters;
using Registry.API.Services;
using Registry.API.Utilities;
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
    [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.OK)]
    public async Task<ApiResult> Post([FromBody] AddRegistryDto registry)
    {
        await service.AddAsync(registry, User.GetId());
        return Ok();
    }

    /// <summary>
    /// Retrieves a filtered list of registry entries.
    /// If no filter is provided, returns all registry entries.
    /// </summary>
    /// <param name="filter">The filtering criteria as a <see cref="FilterRegistryDto"/> object.</param>
    /// <returns>An <see cref="IActionResult"/> containing the filtered list of registry entries.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<FilterRegistryDto>), (int)HttpStatusCode.OK)]
    public async Task<ApiResult<FilterRegistryDto>> Get([FromQuery] FilterRegistryDto filter)
    {
        return Ok(await service.FilterAsync(filter,User.GetId()));
    }
    
    /// <summary>
    /// Retrieves a filtered list of registry entries.
    /// get all registries for supporter
    /// </summary>
    /// <param name="filter">The filtering criteria as a <see cref="FilterRegistryDto"/> object.</param>
    /// <returns>An <see cref="IActionResult"/> containing the filtered list of registry entries.</returns>
    [HttpGet("get-all")]
    [ProducesResponseType(typeof(ApiResult<FilterRegistryDto>), (int)HttpStatusCode.OK)]
    [PermissionChecker("supporter")]
    public async Task<ApiResult<FilterRegistryDto>> GetAll([FromQuery] FilterRegistryDto filter)
    {
        return Ok(await service.FilterAsync(filter,null));
    }
    
    /// <summary>
    /// Processes the registry entry by accepting or rejecting based on provided data.
    /// </summary>
    /// <param name="decisionDto">The registry data to accept or reject, provided as a <see cref="RegistryDecisionDto"/> object.</param>
    /// <returns>An <see cref="ApiResult"/> representing the result of the operation.</returns>
    /// <response code="200">Returns an OK status with the operation result.</response>
    /// <response code="400">If the request data is invalid.</response>
    /// <response code="404">If the registry entry is not found.</response>
    [HttpPut("Decision")]
    [PermissionChecker("supporter")]
    [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.NotFound)]
    public async Task<ApiResult> Decision([FromBody] RegistryDecisionDto decisionDto)
    {
        if (!ModelState.IsValid) return BadRequest();
        
        await service.ProcessRegistryDecisionAsync(decisionDto);
        
        return Ok();
    }

    /// <summary>
    /// Updates the registry entry by accepting the provided registry data.
    /// </summary>
    /// <param name="accept">The registry data to accept, provided as an <see cref="AcceptRegistryDto"/> object.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the update operation, wrapped in an <see cref="ApiResult"/>.</returns>
    /// <response code="200">Returns an OK status with the updated registry data.</response>
    [HttpPut("SendPriceAndLink")]
    [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.OK)]
    public async Task<ApiResult> SendPriceAndLink([FromBody] SendPriceAndLinkForPaymentDto accept)
    {
        await service.SendPriceAndLink(accept); 
        return Ok();
    }

    /// <summary>
    /// Retrieves a unique identifier (UID) for a specific operation or transaction.
    /// </summary>
    /// <returns>An <see cref="ApiResult{T}"/> containing the unique identifier as a string.</returns>
    /// <response code="200">Returns an OK status with the unique identifier.</response>
    /// <response code="500">Returns an Internal Server Error if the UID cannot be generated.</response>
    [HttpGet("SendUniqueId/{id}")]
    [ProducesResponseType(typeof(ApiResult<string>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiResult<string>), (int)HttpStatusCode.InternalServerError)]
    [PermissionChecker("supporter")]
    public async Task<ApiResult<string>> SendUniqueId(long id)
    {
        return await service.GetUniqueIdAsync(id);
    }
}