using backend.Configuration;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace backend.Repositories;

public sealed class DatabaseStatusRepository(
    ApplicationDbContext dbContext,
    IOptions<DatabaseSettings> databaseOptions) : IDatabaseStatusRepository
{
    public async Task<DatabaseStatusResponse> GetStatusAsync(CancellationToken cancellationToken)
    {
        var databaseSettings = databaseOptions.Value;

        try
        {
            var canConnect = await dbContext.Database.CanConnectAsync(cancellationToken);

            return new DatabaseStatusResponse(
                Provider: "MySQL",
                Host: databaseSettings.Host,
                Port: databaseSettings.Port,
                Database: databaseSettings.Name,
                CanConnect: canConnect,
                Error: canConnect ? null : "EF Core could not connect to the configured MySQL database.");
        }
        catch (Exception exception)
        {
            return new DatabaseStatusResponse(
                Provider: "MySQL",
                Host: databaseSettings.Host,
                Port: databaseSettings.Port,
                Database: databaseSettings.Name,
                CanConnect: false,
                Error: exception.Message);
        }
    }
}