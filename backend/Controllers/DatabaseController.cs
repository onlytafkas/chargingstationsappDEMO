using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class DatabaseController(IDatabaseStatusService databaseStatusService) : ControllerBase
{
    [HttpGet("status")]
    [ProducesResponseType(typeof(DatabaseStatusResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<DatabaseStatusResponse>> GetStatus(CancellationToken cancellationToken)
    {
        var status = await databaseStatusService.GetStatusAsync(cancellationToken);
        return Ok(status);
    }
}