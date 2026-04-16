using backend.Models;

namespace backend.Services;

public interface IDatabaseStatusService
{
    Task<DatabaseStatusResponse> GetStatusAsync(CancellationToken cancellationToken);
}