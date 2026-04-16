namespace backend.Models;

public sealed record LoadingSessionResponse(
    int Id,
    string UserEmail,
    DateTime StartDateTime,
    DateTime EndDateTime,
    string StationId);