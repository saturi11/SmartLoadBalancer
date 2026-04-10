using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

var app = builder.Build();

var instanceId = Environment.GetEnvironmentVariable("INSTANCE_ID")
                 ?? Guid.NewGuid().ToString();

app.MapGet("/ping", async () =>
{
    await Task.Delay(200); // Simulate latency

    return Results.Ok(new
    {
        instance = instanceId,
        timestamp = DateTime.UtcNow
    });
});

app.MapGet("/health", () => Results.Ok("healthy"));

app.Run();