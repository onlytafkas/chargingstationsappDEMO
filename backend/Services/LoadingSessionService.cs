using backend.Models;
using backend.Repositories;

namespace backend.Services;

public sealed class LoadingSessionService(ILoadingSessionRepository loadingSessionRepository) : ILoadingSessionService
{
    public async Task<IReadOnlyList<LoadingSessionResponse>> GetSessionsAsync(CancellationToken cancellationToken)
    {
        var sessions = await loadingSessionRepository.GetSessionsAsync(cancellationToken);

        return sessions
            .Select(session => new LoadingSessionResponse(
                session.Id,
                session.UserEmail,
                session.StartDateTime,
                session.EndDateTime,
                session.StationId))
            .ToList();
    }
}