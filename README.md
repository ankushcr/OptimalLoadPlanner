# SmartLoad Optimization API

High-performance load optimization service that selects the optimal combination of compatible orders for a truck, maximizing carrier payout while respecting operational constraints.

---

## 🚀 Overview

This service solves a constrained optimization problem:

- Maximize total `payout_cents`
- Respect truck weight & volume limits
- Enforce hazmat compatibility (no mixing)
- Ensure route compatibility (same origin & destination)
- Validate pickup and delivery dates
- Negative or invalid values → HTTP 400
- Too many orders → HTTP 413
- Stateless, in-memory processing
- Supports up to 22 orders (`2²²` state exploration)

---

## 🛠 Tech Stack

- .NET 8
- ASP.NET Core Web API
- Pure in-memory optimization
- Multi-stage Docker build
- Stateless service
- Port 8080

---

## 📦 How to Run

### Clone Repository

```bash
git clone https://github.com/ankushcr/OptimalLoadPlanner.git
cd OptimalLoadPlanner
```

### Run with Docker

```bash
docker compose up --build
```

- Service will be available at:
```bash
http://localhost:8080
```

---

## ❤️ Health Check

```bash
curl http://localhost:8080/healthz
```

- Expected Response:
```bash
Healthy
```

---

## 📬 Optimization Endpoint

- POST

```bash
/api/v1/load-optimizer/optimize
```

- Example Request

```json
curl -X POST http://localhost:8080/api/v1/load-optimizer/optimize \
-H "Content-Type: application/json" \
-d @sample-request.json
```

---

## 📄 Project Structure

```
SmartLoad/
│
├── SmartLoad.Api/
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   └── Program.cs
│	└── Dockerfile
├── docker-compose.yml
└── README.md
```

---

## 📄 Sample JSON Request

```json
{
  "truck": {
    "id": "truck-123",
    "max_weight_lbs": 44000,
    "max_volume_cuft": 3000
  },
  "orders": [
    {
      "id": "ord-001",
      "payout_cents": 250000,
      "weight_lbs": 18000,
      "volume_cuft": 1200,
      "origin": "Los Angeles, CA",
      "destination": "Dallas, TX",
      "pickup_date": "2025-12-05",
      "delivery_date": "2025-12-09",
      "is_hazmat": false
    }
  ]
}
```

---

## ✅ Example Response

```json
{
  "truck_id": "truck-123",
  "selected_order_ids": ["ord-001"],
  "total_payout_cents": 250000,
  "total_weight_lbs": 18000,
  "total_volume_cuft": 1200,
  "utilization_weight_percent": 40.91,
  "utilization_volume_percent": 40.0
}
```

---