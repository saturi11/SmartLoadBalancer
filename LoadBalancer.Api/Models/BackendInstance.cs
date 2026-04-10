namespace LoadBalancer.Api.Models
{
    public class BackendInstance
    {
        public string Url { get; set; } = default!;
        public bool IsHealthy { get; set; } = true;
        private int _activeConnections;
        private int _totalRequests;
        private int _failedRequests;
        private long _totalResponseTime;

        public int ActiveConnections => _activeConnections;
        public int TotalRequests => _totalRequests;
        public int FailedRequests => _failedRequests;
        public long TotalResponseTime => _totalResponseTime;

        public void Increment() => Interlocked.Increment(ref _activeConnections);
        public void Decrement() => Interlocked.Decrement(ref _activeConnections);
        public void IncrementConnections() => Interlocked.Increment(ref _activeConnections);
        public void DecrementConnections() => Interlocked.Decrement(ref _activeConnections);

        public void IncrementRequests() => Interlocked.Increment(ref _totalRequests);
        public void IncrementFailures() => Interlocked.Increment(ref _failedRequests);

        public void AddResponseTime(long ms) => Interlocked.Add(ref _totalResponseTime, ms);

        public double AverageResponseTime =>
            _totalRequests == 0 ? 0 : (double)_totalResponseTime / _totalRequests;


    }
}
