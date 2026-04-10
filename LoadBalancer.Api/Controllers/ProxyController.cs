namespace LoadBalancer.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using LoadBalancer.Api.Services;

    [ApiController]
    [Route("{**catch-all}")]
    public class ProxyController : ControllerBase
    {
        private readonly LoadBalancerService _lb;
        private readonly HttpClient _httpClient;

        public ProxyController(LoadBalancerService lb, IHttpClientFactory factory)
        {
            _lb = lb;
            _httpClient = factory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> Forward()
        {
            var instances = _lb.GetAllInstances()
                               .Where(i => i.IsHealthy)
                               .ToList();

            if (!instances.Any())
                return StatusCode(503, "No healthy instances");

            var attempts = instances.Count;

            for (int i = 0; i < attempts; i++)
            {
                var instance = _lb.GetNextInstance();

                try
                {
                    instance.Increment();

                    Console.WriteLine($"[{instance.Url}] Active: {instance.ActiveConnections}");

                    var response = await _httpClient.GetAsync($"{instance.Url}/ping");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return Content(content, "application/json");
                    }
                }
                catch
                {
                    Console.WriteLine($"Failed: {instance.Url}");
                }
                finally
                {
                    instance.Decrement();
                }
            }

            return StatusCode(503, "All instances failed");
        }
    }
}
