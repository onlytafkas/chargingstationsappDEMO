using System.Runtime.CompilerServices;
using backend.Models;
using backend.Repositories;

namespace backend.Services;

public sealed class DatabaseStatusService(IDatabaseStatusRepository databaseStatusRepository) : IDatabaseStatusService
{
    public Task<DatabaseStatusResponse> GetStatusAsync(CancellationToken cancellationToken)
    {
        return databaseStatusRepository.GetStatusAsync(cancellationToken);
    }
}