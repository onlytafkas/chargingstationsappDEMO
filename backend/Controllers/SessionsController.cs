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

    [HttpPost]
    [ProducesResponseType(typeof(LoadingSessionResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoadingSessionResponse>> CreateSession(
        [FromBody] CreateLoadingSessionRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var created = await loadingSessionService.CreateSessionAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetSessions), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}