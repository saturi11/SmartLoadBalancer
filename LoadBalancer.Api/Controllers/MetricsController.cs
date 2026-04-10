using Microsoft.AspNetCore.Mvc;
using LoadBalancer.Api.Services;

namespace LoadBalancer.Api.Controllers
{
   
    [ApiController]
    [Route("metrics")]
    public class MetricsController : ControllerBase
    {
        private readonly LoadBalancerService _lb;

        public MetricsController(LoadBalancerService lb)
        {
            _lb = lb;
        }

        [HttpGet]
        public IActionResult GetMetrics()
        {
            var data = _lb.GetAllInstances().Select(i => new
            {
                url = i.Url,
                activeConnections = i.ActiveConnections,
                totalRequests = i.TotalRequests,
                failedRequests = i.FailedRequests,
                avgResponseTime = i.AverageResponseTime
            });

            return Ok(data);
        }
    }
}
