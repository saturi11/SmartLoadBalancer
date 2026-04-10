using LoadBalancer.Api.Services;
using LoadBalancer.Api.Strategies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddSingleton<ILoadBalancingStrategy, LeastConnectionsStrategy>();
builder.Services.AddSingleton<LoadBalancerService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();