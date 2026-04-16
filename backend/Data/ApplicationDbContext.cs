using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
}