using System.Net;
using Microsoft.AspNetCore.Mvc;
using Registry.API.Filters;
using Registry.API.Models;
using Registry.API.Services;
using Registry.API.ViewModel;

namespace Registry.API.Controllers;

public class RejectionReasonsController(IPredefinedRejectionReasonService service) : DefaultController
{
    /// <summary>
    /// Retrieves all predefined rejection reasons.
    /// </summary>
    /// <returns>List of predefined rejection reasons.</returns>
    [HttpGet("predefined")]
    [ProducesResponseType(typeof(ApiResult<List<PredefinedRejectionReasonDto>>),(int)HttpStatusCode.OK)]
    [ProducesDefaultResponseType]
    public async Task<ApiResult<List<PredefinedRejectionReasonDto>>> GetPredefinedRejectionReasons()
    {
        return Ok(await service.GetAsync());
    }
}