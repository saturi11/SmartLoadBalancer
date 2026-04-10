var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

var instanceId = Environment.GetEnvironmentVariable("INSTANCE_ID")
                 ?? Guid.NewGuid().ToString();

app.MapGet("/ping", () =>
{
    return Results.Ok(new
    {
        instance = instanceId,
        timestamp = DateTime.UtcNow
    });
});

app.MapGet("/health", () => Results.Ok("healthy"));

app.Run();