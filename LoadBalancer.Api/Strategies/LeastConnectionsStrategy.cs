using LoadBalancer.Api.Models;
namespace LoadBalancer.Api.Strategies
{

    public class LeastConnectionsStrategy : ILoadBalancingStrategy
    {
        public BackendInstance SelectInstance(List<BackendInstance> instances)
        {
            return instances
                .OrderBy(i => i.ActiveConnections)
                .ThenBy(_ => Guid.NewGuid())
                .First();
        }
    }
}
