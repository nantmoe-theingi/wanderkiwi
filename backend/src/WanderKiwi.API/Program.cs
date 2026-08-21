using WanderKiwi.Application.Interfaces;
using WanderKiwi.Application.Services;
using WanderKiwi.Infrastructure.Data;
using WanderKiwi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using WandarKiwi.Application.Interfaces;

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
builder.Services.AddDbContext<WanderKiwiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Register the Repository and Service for Dependency Injection
builder.Services.AddScoped<IAttractionService, AttractionService>();
builder.Services.AddScoped<IAttractionRepository, AttractionRepository>();
builder.Services.AddScoped<IDestinationRepository, DestinationRepository>();
builder.Services.AddScoped<IDestinationService, DestinationService>();

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