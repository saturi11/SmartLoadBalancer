namespace LoadBalancer.Api.Services
{
    using LoadBalancer.Api.Models;
    using LoadBalancer.Api.Strategies;

    public class LoadBalancerService
    {
        private readonly List<BackendInstance> _instances;
        private readonly ILoadBalancingStrategy _strategy;

        public LoadBalancerService(ILoadBalancingStrategy strategy)
        {
            _strategy = strategy;

            _instances = new List<BackendInstance>
            {
                new() { Url = "http://backend-1:8080" },
                new() { Url = "http://backend-2:8080" },
                new() { Url = "http://backend-3:8080" }
            };
        }

        public BackendInstance GetNextInstance()
        {
            var healthyInstances = _instances.Where(i => i.IsHealthy).ToList();

            if (!healthyInstances.Any())
                throw new Exception("No healthy instances available");

            return _strategy.SelectInstance(healthyInstances);
        }

        public List<BackendInstance> GetAllInstances() => _instances;
    }
}
