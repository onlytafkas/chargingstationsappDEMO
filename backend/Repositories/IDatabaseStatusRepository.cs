using backend.Models;

namespace backend.Repositories;

public interface IDatabaseStatusRepository
{
    Task<DatabaseStatusResponse> GetStatusAsync(CancellationToken cancellationToken);
}