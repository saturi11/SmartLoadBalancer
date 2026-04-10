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
            var instance = _lb.GetNextInstance();

            var response = await _httpClient.GetAsync($"{instance.Url}/ping");

            var content = await response.Content.ReadAsStringAsync();

            return Content(content, "application/json");
        }
    }
}
