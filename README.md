# 🚀 Smart Load Balancer (.NET 8)

A production-inspired **custom load balancer** built with **.NET 8**, implementing real-world concepts such as **load balancing strategies**, **failover**, **retry mechanisms**, **health checks**, **metrics**, and a fully **containerized distributed environment using Docker**.

---

# 🎯 Purpose

This project simulates how real-world load balancers operate internally, focusing on:

* High availability
* Fault tolerance
* Intelligent request distribution
* Observability
* Distributed system behavior

---

# 🧠 Architecture Overview

```
                ┌────────────────────┐
                │   Load Balancer    │
                │   (.NET API)       │
                └─────────┬──────────┘
                          │
        ┌─────────────────┼─────────────────┐
        │                 │                 │
┌──────────────┐ ┌──────────────┐ ┌──────────────┐
│ Backend #1   │ │ Backend #2   │ │ Backend #3   │
│ (.NET API)   │ │ (.NET API)   │ │ (.NET API)   │
└──────────────┘ └──────────────┘ └──────────────┘
```

---

# 🏗️ Technologies Used

* **.NET 8** → Core backend framework
* **ASP.NET Core** → API development
* **Docker** → Containerization
* **Docker Compose** → Multi-container orchestration
* **HttpClient** → Inter-service communication
* **Interlocked (Threading)** → Thread-safe counters
* **Swagger** → API documentation

---

# ⚙️ Features Implemented

## 🔁 Load Balancing Strategies

### 1. Round Robin

* Cycles through instances sequentially
* Simple and predictable

### 2. Least Connections ⭐

* Sends traffic to the instance with the lowest active load
* More efficient under concurrency

---

## 🔄 Retry Mechanism (Failover)

* If a request fails:

  * Automatically retries on another instance

```
Instance A ❌ → Instance B ✅
```

---

## ❤️ Health Check System

* Background service continuously monitors instances
* Marks instances as:

  * ✅ Healthy
  * ❌ Unhealthy
* Load balancer avoids unhealthy instances

---

## 📊 Metrics & Observability

Per-instance tracking:

* Total requests
* Failed requests
* Active connections
* Average response time

Example:

```json
[
  {
    "url": "http://backend-1:8080",
    "totalRequests": 10,
    "failedRequests": 0,
    "avgResponseTime": 220
  }
]
```

---

## 🧵 Concurrency Handling

* Thread-safe counters using `Interlocked`
* Prevents race conditions

---

## 🐳 Dockerized Cluster

* 1 Load Balancer container
* 3 Backend containers
* Internal Docker network communication

```
backend-1
backend-2
backend-3
```

---

# 📁 Project Structure

```
SmartLoadBalancer/
│
├── LoadBalancer.Api/
│   ├── Controllers/
│   ├── Services/
│   ├── Strategies/
│   └── Models/
│
├── Backend.Service/
│   └── Minimal API
│
├── docker-compose.yml
```

---

# 🧠 Architectural Decisions

## 🎯 Why Custom Load Balancer?

* Full control over logic
* Deep understanding of distributed systems
* Demonstrates system design skills

---

## 🔁 Why Strategy Pattern?

* Allows dynamic switching of algorithms
* Follows Open/Closed Principle
* Improves maintainability

---

## 🧵 Why Thread-Safe Counters?

Without this:

```
Race conditions → incorrect load distribution
```

Solution:

```
Interlocked.Increment()
```

---

## 🔄 Why Retry Across Instances?

```
Failure → fallback → system continues
```

Improves:

* Availability
* Reliability

---

## ❤️ Why Health Checks?

* Prevents routing traffic to broken services

---

## 📊 Why Metrics?

* Enables performance monitoring
* Supports debugging and scaling decisions

---

## 🐳 Why Docker?

* Simulates real environments
* Ensures reproducibility
* Enables distributed architecture

---

# 🚀 How to Run

## 1. Build

```bash
docker-compose build
```

## 2. Run

```bash
docker-compose up
```

## 3. Test

```
http://localhost:5000/ping
```

## 4. Metrics

```
http://localhost:5000/metrics
```

---

# 🧪 Testing Scenarios

## Load Test

```powershell
1..20 | % { Invoke-RestMethod http://localhost:5000/ping }
```

---

## Failover Test

```bash
docker stop backend-2
```

Expected:

* System continues working
* No errors

---

## Stress Test

```powershell
1..50 | % { Start-Job { Invoke-RestMethod http://localhost:5000/ping } }
```

---

# 📈 Results

* Balanced distribution
* Stable response times
* No downtime under failure
* Accurate metrics

---

# 🧠 What This Project Demonstrates

* Distributed systems design
* Fault tolerance
* Load balancing algorithms
* Concurrency handling
* Observability
* Containerization

---

# 🔥 Possible Improvements

* Circuit Breaker (Polly)
* Prometheus + Grafana
* OpenTelemetry
* Kubernetes
* Auto-scaling

---

# 👨‍💻 Author

Gabriel Saturi

---

# ⭐ Final Thoughts

```
This is not just an API.
This is a distributed system simulation.
```

🚀 Production-inspired. Architecture-driven. Portfolio-ready.
