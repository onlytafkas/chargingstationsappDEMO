using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class SessionsController(ILoadingSessionService loadingSessionService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<LoadingSessionResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<LoadingSessionResponse>>> GetSessions(CancellationToken cancellationToken)
    {
        var sessions = await loadingSessionService.GetSessionsAsync(cancellationToken);
        return Ok(sessions);
    }
}