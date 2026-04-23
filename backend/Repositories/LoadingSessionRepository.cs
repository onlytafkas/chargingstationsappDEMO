using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public sealed class LoadingSessionRepository(ApplicationDbContext dbContext) : ILoadingSessionRepository
{
    public async Task<IReadOnlyList<LoadingSession>> GetSessionsAsync(CancellationToken cancellationToken)
    {
        return await dbContext.LoadingSessions
            .AsNoTracking()
            .OrderBy(session => session.StartDateTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<LoadingSession> CreateSessionAsync(LoadingSession session, CancellationToken cancellationToken)
    {
        dbContext.LoadingSessions.Add(session);
        await dbContext.SaveChangesAsync(cancellationToken);
        return session;
    }
}