namespace backend.Models;

public sealed record DatabaseStatusResponse(
    string Provider,
    string Host,
    uint Port,
    string Database,
    bool CanConnect,
    string? Error);