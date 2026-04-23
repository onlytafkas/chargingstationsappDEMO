using backend.Models;

namespace backend.Services;

public interface ILoadingSessionService
{
    Task<IReadOnlyList<LoadingSessionResponse>> GetSessionsAsync(CancellationToken cancellationToken);

    Task<LoadingSessionResponse> CreateSessionAsync(CreateLoadingSessionRequest request, CancellationToken cancellationToken);
}