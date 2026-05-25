using Microsoft.AspNetCore.Mvc;
using WebApplicationCF.DTOs;
using WebApplicationCF.Services;

namespace WebApplicationCF.Controllers;

[ApiController]
[Route("api/pcs")]
public class PCsController(IPCsService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PCResponseDto>>> GetPCs(CancellationToken cancellationToken)
    {
        return Ok(await service.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:int}/components")]
    public async Task<ActionResult<PCDetailsResponseDto>> GetPCComponents(
        int id,
        CancellationToken cancellationToken)
    {
        var pc = await service.GetByIdAsync(id, cancellationToken);

        if (pc is null)
        {
            return NotFound();
        }

        return Ok(pc);
    }

    [HttpPost]
    public async Task<ActionResult<PCResponseDto>> AddPC(
        CreatePCRequestDto request,
        CancellationToken cancellationToken)
    {
        var pc = await service.AddAsync(request, cancellationToken);

        return CreatedAtAction(nameof(GetPCComponents), new { id = pc.Id }, pc);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePC(
        int id,
        UpdatePCRequestDto request,
        CancellationToken cancellationToken)
    {
        var wasUpdated = await service.UpdateAsync(id, request, cancellationToken);

        if (!wasUpdated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePC(int id, CancellationToken cancellationToken)
    {
        var wasDeleted = await service.DeleteAsync(id, cancellationToken);

        if (!wasDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
