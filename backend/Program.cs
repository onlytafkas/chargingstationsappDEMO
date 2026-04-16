using backend.Configuration;
using backend.Data;
using backend.Repositories;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

DotEnvLoader.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(DatabaseSettings.SectionName));
builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
{
    var databaseSettings = serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    options.UseMySql(databaseSettings.ConnectionString, databaseSettings.GetServerVersion());
});
builder.Services.AddScoped<IDatabaseStatusRepository, DatabaseStatusRepository>();
builder.Services.AddScoped<IDatabaseStatusService, DatabaseStatusService>();
builder.Services.AddScoped<ILoadingSessionRepository, LoadingSessionRepository>();
builder.Services.AddScoped<ILoadingSessionService, LoadingSessionService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
