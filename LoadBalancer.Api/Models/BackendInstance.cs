namespace LoadBalancer.Api.Models
{
    public class BackendInstance
    {
        public string Url { get; set; } = default!;
        public bool IsHealthy { get; set; } = true;
        public int ActiveConnections { get; set; } = 0;
    }
}
