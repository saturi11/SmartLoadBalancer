namespace LoadBalancer.Api.Strategies
{
    using LoadBalancer.Api.Models;

    public class RoundRobinStrategy : ILoadBalancingStrategy
    {
        private int _index = 0;
        private readonly object _lock = new();

        public BackendInstance SelectInstance(List<BackendInstance> instances)
        {
            lock (_lock)
            {
                var instance = instances[_index % instances.Count];
                _index++;
                return instance;
            }
        }
    }
}
