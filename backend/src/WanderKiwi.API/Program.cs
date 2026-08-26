using WanderKiwi.Application.Interfaces;
using WanderKiwi.Application.Services;
using WanderKiwi.Infrastructure.Data;
using WanderKiwi.Infrastructure.Repositories;
using WanderKiwi.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using WandarKiwi.Application.Interfaces;
using WanderKiwi.Application.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 1. Define a specific CORS policy
var AllowAngularApp = "_allowAngularApp";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowAngularApp,
        policy =>
        {
            policy.WithOrigins(
                    "http://localhost:4200",                                    // Local Angular dev server
                    "https://nantmoe-theingi.github.io"                         // Your GitHub Pages production domain
                  )
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Register the DbContext with a connection string
// 1a. Get connection string safely from config or environment variables
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") 
                       ?? Environment.GetEnvironmentVariable("DATABASE_URL");
}

// Convert Railway's URL format if present (handling both postgres:// and postgresql://)
if (!string.IsNullOrEmpty(connectionString) && 
    (connectionString.StartsWith("postgres://") || connectionString.StartsWith("postgresql://")))
{
    var uri = new Uri(connectionString);
    var userInfo = uri.UserInfo.Split(':');
    connectionString = $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";
}

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("CRITICAL: Connection string is completely null or empty!");
}

builder.Services.AddDbContext<WanderKiwiDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add standard in-memory caching
builder.Services.AddMemoryCache();

// 2. Register the Repository and Service for Dependency Injection
builder.Services.AddScoped<IAttractionService, AttractionService>();
builder.Services.AddScoped<IAttractionRepository, AttractionRepository>();
builder.Services.AddScoped<IDestinationRepository, DestinationRepository>();
builder.Services.AddScoped<IDestinationService, DestinationService>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<ITripService, TripService>();
// builder.Services.AddScoped<ITripGenerationService, TripGenerationService>();
builder.Services.Configure<OpenRouteServiceOptions>(builder.Configuration.GetSection(OpenRouteServiceOptions.SectionName));
builder.Services.AddHttpClient<IRouteService, OpenRouteService>(client =>
    client.BaseAddress = new Uri("https://api.openrouteservice.org/"));
builder.Services.Configure<GroqApiOptions>(
    builder.Configuration.GetSection("GroqApi"));

builder.Services.AddHttpClient<GroqTripGenerationService>();

builder.Services.AddScoped<ITripGenerationService, GroqTripGenerationService>();


var app = builder.Build();

// Automatically apply migrations and seed data on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WanderKiwiDbContext>();
    dbContext.Database.Migrate(); // Applies any unapplied migrations safely
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 2. Enable the CORS middleware (Must be placed BEFORE UseAuthorization and MapControllers)
app.UseCors(AllowAngularApp);

app.UseAuthorization();

app.MapControllers();

app.Run();
