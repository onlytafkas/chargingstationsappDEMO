using Microsoft.EntityFrameworkCore;

namespace backend.Configuration;

public sealed class DatabaseSettings
{
    public const string SectionName = "Database";

    public string Host { get; init; } = "localhost";

    public uint Port { get; init; } = 3306;

    public string Name { get; init; } = "chargingstationsappDEMO";

    public string User { get; init; } = "root";

    public string Password { get; init; } = string.Empty;

    public string ServerVersion { get; init; } = "8.0.36";

    public string ConnectionString =>
        $"Server={Host};Port={Port};Database={Name};User ID={User};Password={Password};";

    public MySqlServerVersion GetServerVersion()
    {
        if (!Version.TryParse(ServerVersion, out var version))
        {
            throw new InvalidOperationException(
                $"Database__ServerVersion must be a valid version number, but was '{ServerVersion}'.");
        }

        return new MySqlServerVersion(version);
    }
}