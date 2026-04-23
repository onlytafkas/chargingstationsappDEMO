using backend.Models;

namespace backend.Repositories;

public interface ILoadingSessionRepository
{
    Task<IReadOnlyList<LoadingSession>> GetSessionsAsync(CancellationToken cancellationToken);

    Task<LoadingSession> CreateSessionAsync(LoadingSession session, CancellationToken cancellationToken);
}