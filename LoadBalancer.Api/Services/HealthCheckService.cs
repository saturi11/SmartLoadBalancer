namespace LoadBalancer.Api.Services
{
    using LoadBalancer.Api.Models;

    public class HealthCheckService : BackgroundService
    {
        private readonly LoadBalancerService _lb;
        private readonly HttpClient _httpClient;

        public HealthCheckService(LoadBalancerService lb, IHttpClientFactory factory)
        {
            _lb = lb;
            _httpClient = factory.CreateClient();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var instances = _lb.GetAllInstances();

                foreach (var instance in instances)
                {
                    try
                    {
                        var response = await _httpClient.GetAsync($"{instance.Url}/health", stoppingToken);

                        instance.IsHealthy = response.IsSuccessStatusCode;
                    }
                    catch
                    {
                        instance.IsHealthy = false;
                    }
                }

                await Task.Delay(3000, stoppingToken); // roda a cada 3s
            }
        }
    }
}
