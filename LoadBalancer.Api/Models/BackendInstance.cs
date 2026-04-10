namespace LoadBalancer.Api.Models
{
    public class BackendInstance
    {
        public string Url { get; set; } = default!;
        public bool IsHealthy { get; set; } = true;
        private int _activeConnections;

        public int ActiveConnections => _activeConnections;

        public void Increment() => Interlocked.Increment(ref _activeConnections);
        public void Decrement() => Interlocked.Decrement(ref _activeConnections);
    }
}
