using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Registry.API.Repositories.Interfaces;
using Registry.API.ViewModel;

namespace Registry.API.Controllers;

public class RegistryController(IRegistryRepository repository, IMapper mapper) : DefaultController
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddRegistryDto registry)
    {
        var main = mapper.Map<Models.Registry>(registry);

        if (await repository.ExistsAsync(x => x.ImeI_1 == registry.ImeI_1 || x.ImeI_2 == registry.ImeI_2))
            throw new ValidationException("شماره IMEI وارد شده قبلاً ثبت شده است.");
        
        await repository.AddAsync(main);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await repository.GetAllAsync());
    }
}