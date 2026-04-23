using backend.Models;
using backend.Repositories;
using backend.Services;
using NSubstitute;
using Xunit;

namespace backend.Tests.Services;

public sealed class LoadingSessionServiceTests
{
    private readonly ILoadingSessionRepository _repository;
    private readonly LoadingSessionService _sut;

    public LoadingSessionServiceTests()
    {
        _repository = Substitute.For<ILoadingSessionRepository>();
        _sut = new LoadingSessionService(_repository);
    }

    // ── GetSessionsAsync ──────────────────────────────────────────────────────

    [Fact]
    public async Task GetSessionsAsync_EmptyRepository_ReturnsEmptyList()
    {
        _repository
            .GetSessionsAsync(Arg.Any<CancellationToken>())
            .Returns(new List<LoadingSession>());

        var result = await _sut.GetSessionsAsync(CancellationToken.None);

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetSessionsAsync_WithSessions_MapsToResponseCorrectly()
    {
        var start = new DateTime(2026, 5, 1, 9, 0, 0);
        var end = new DateTime(2026, 5, 1, 10, 0, 0);

        var sessions = new List<LoadingSession>
        {
            new()
            {
                Id = 42,
                UserEmail = "user@example.com",
                StartDateTime = start,
                EndDateTime = end,
                StationId = "STATION-01"
            }
        };

        _repository
            .GetSessionsAsync(Arg.Any<CancellationToken>())
            .Returns(sessions);

        var result = await _sut.GetSessionsAsync(CancellationToken.None);

        var response = Assert.Single(result);
        Assert.Equal(42, response.Id);
        Assert.Equal("user@example.com", response.UserEmail);
        Assert.Equal(start, response.StartDateTime);
        Assert.Equal(end, response.EndDateTime);
        Assert.Equal("STATION-01", response.StationId);
    }

    // ── CreateSessionAsync ────────────────────────────────────────────────────

    [Fact]
    public async Task CreateSessionAsync_StartDateTimeInPast_ThrowsArgumentException()
    {
        var request = new CreateLoadingSessionRequest
        {
            UserEmail = "user@example.com",
            StartDateTime = DateTime.Now.AddDays(-1),
            EndDateTime = DateTime.Now.AddDays(1),
            StationId = "STATION-01"
        };

        await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.CreateSessionAsync(request, CancellationToken.None));
    }

    [Fact]
    public async Task CreateSessionAsync_EndDateTimeBeforeStart_ThrowsArgumentException()
    {
        var start = DateTime.Now.AddDays(1);
        var request = new CreateLoadingSessionRequest
        {
            UserEmail = "user@example.com",
            StartDateTime = start,
            EndDateTime = start.AddHours(-1),
            StationId = "STATION-01"
        };

        await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.CreateSessionAsync(request, CancellationToken.None));
    }

    [Fact]
    public async Task CreateSessionAsync_EndDateTimeEqualToStart_ThrowsArgumentException()
    {
        var start = DateTime.Now.AddDays(1);
        var request = new CreateLoadingSessionRequest
        {
            UserEmail = "user@example.com",
            StartDateTime = start,
            EndDateTime = start,
            StationId = "STATION-01"
        };

        await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.CreateSessionAsync(request, CancellationToken.None));
    }

    [Fact]
    public async Task CreateSessionAsync_ValidRequest_CallsRepositoryWithCorrectSession()
    {
        var start = DateTime.Now.AddDays(1);
        var end = start.AddHours(2);

        var request = new CreateLoadingSessionRequest
        {
            UserEmail = "user@example.com",
            StartDateTime = start,
            EndDateTime = end,
            StationId = "STATION-01"
        };

        var createdSession = new LoadingSession
        {
            Id = 99,
            UserEmail = request.UserEmail,
            StartDateTime = start,
            EndDateTime = end,
            StationId = request.StationId
        };

        _repository
            .CreateSessionAsync(Arg.Any<LoadingSession>(), Arg.Any<CancellationToken>())
            .Returns(createdSession);

        await _sut.CreateSessionAsync(request, CancellationToken.None);

        await _repository
            .Received(1)
            .CreateSessionAsync(
                Arg.Is<LoadingSession>(s =>
                    s.UserEmail == request.UserEmail &&
                    s.StartDateTime == start &&
                    s.EndDateTime == end &&
                    s.StationId == request.StationId),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task CreateSessionAsync_ValidRequest_ReturnsResponseMappedFromRepository()
    {
        var start = DateTime.Now.AddDays(1);
        var end = start.AddHours(2);

        var request = new CreateLoadingSessionRequest
        {
            UserEmail = "user@example.com",
            StartDateTime = start,
            EndDateTime = end,
            StationId = "STATION-01"
        };

        var createdSession = new LoadingSession
        {
            Id = 99,
            UserEmail = request.UserEmail,
            StartDateTime = start,
            EndDateTime = end,
            StationId = request.StationId
        };

        _repository
            .CreateSessionAsync(Arg.Any<LoadingSession>(), Arg.Any<CancellationToken>())
            .Returns(createdSession);

        var result = await _sut.CreateSessionAsync(request, CancellationToken.None);

        Assert.Equal(99, result.Id);
        Assert.Equal("user@example.com", result.UserEmail);
        Assert.Equal(start, result.StartDateTime);
        Assert.Equal(end, result.EndDateTime);
        Assert.Equal("STATION-01", result.StationId);
    }
}
