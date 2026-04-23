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

    public async Task<LoadingSessionResponse> CreateSessionAsync(CreateLoadingSessionRequest request, CancellationToken cancellationToken)
    {
        if (request.StartDateTime < DateTime.Now)
            throw new ArgumentException("Start date/time cannot be in the past.", nameof(request));

        if (request.EndDateTime <= request.StartDateTime)
            throw new ArgumentException("End date/time must be after start date/time.", nameof(request));

        var session = new LoadingSession
        {
            UserEmail = request.UserEmail,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            StationId = request.StationId,
        };

        var created = await loadingSessionRepository.CreateSessionAsync(session, cancellationToken);

        return new LoadingSessionResponse(
            created.Id,
            created.UserEmail,
            created.StartDateTime,
            created.EndDateTime,
            created.StationId);
    }
}