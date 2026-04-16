using backend.Models;

namespace backend.Services;

public interface ILoadingSessionService
{
    Task<IReadOnlyList<LoadingSessionResponse>> GetSessionsAsync(CancellationToken cancellationToken);
}