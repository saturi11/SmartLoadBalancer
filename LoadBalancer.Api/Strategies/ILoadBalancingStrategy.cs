using LoadBalancer.Api.Models;

namespace LoadBalancer.Api.Strategies
{
    public interface ILoadBalancingStrategy
    {
        BackendInstance SelectInstance(List<BackendInstance> instances);
    }
}
