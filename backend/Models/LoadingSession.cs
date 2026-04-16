namespace backend.Models;

public sealed class LoadingSession
{
    public int Id { get; set; }

    public required string UserEmail { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public required string StationId { get; set; }
}