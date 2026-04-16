using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
	public DbSet<LoadingSession> LoadingSessions => Set<LoadingSession>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<LoadingSession>(entity =>
		{
			entity.ToTable("sessions");

			entity.HasKey(session => session.Id);

			entity.Property(session => session.Id)
				.HasColumnName("id")
				.ValueGeneratedOnAdd();

			entity.Property(session => session.UserEmail)
				.HasColumnName("user_email")
				.HasMaxLength(320)
				.IsRequired();

			entity.Property(session => session.StartDateTime)
				.HasColumnName("start_datetime")
				.HasColumnType("datetime")
				.IsRequired();

			entity.Property(session => session.EndDateTime)
				.HasColumnName("end_datetime")
				.HasColumnType("datetime")
				.IsRequired();

			entity.Property(session => session.StationId)
				.HasColumnName("stationid")
				.HasMaxLength(100)
				.IsRequired();
		});
	}
}