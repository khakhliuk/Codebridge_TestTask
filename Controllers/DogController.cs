using Codebridge_TestTask.Interfaces;
using Codebridge_TestTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace Codebridge_TestTask.Controllers;

[Route("dogs")]
[ApiController]
public class DogController : ControllerBase
{
    private readonly IDogService _dogService;
    
    public DogController(IDogService dogService)
    {
        _dogService = dogService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] SearchQuery searchQuery)
    {
        return Ok(await _dogService.GetAsync(searchQuery));
    }

    [HttpPost]
    [Route("/dog")]
    public async Task<IActionResult> AddAsync([FromBody] CreateDogRequest createDogRequest, CancellationToken cancellationToken)
    {
        return Ok(await _dogService.AddAsync(createDogRequest, cancellationToken));
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateDogRequest updateDogRequest,
        CancellationToken cancellationToken)
    {
        return Ok(await _dogService.UpdateAsync(updateDogRequest, cancellationToken));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        return Ok(await _dogService.DeleteAsync(id, cancellationToken));
    }
}