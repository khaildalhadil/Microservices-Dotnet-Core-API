# eCommerce Microservices — .NET 10

An e-commerce backend split into three independent microservices. Each service owns its own database, and all of them start together with a single Docker Compose command.

| Service | Architecture | Database | Data access | Port (host) |
|---|---|---|---|---|
| **Products** | Minimal API, 3 layers (API / BLL / DAL) | MySQL 8 | EF Core | `8080` |
| **Users** | Clean Architecture (API / Core / Infrastructure) | PostgreSQL 17 | Dapper | `9090` |
| **Orders** | Controllers, 3 layers (API / BLL / DAL) | MongoDB 8 | MongoDB.Driver | `7000` |

Prices are stored in Omani Rial (3 decimal places), and every database is seeded automatically on first start.

## Architecture

```
                 ┌──────────────────────────────────────────────┐
                 │              ecommerce-network               │
                 │                                              │
  :8080 ──▶ Products API ──▶ MySQL        (mysql-init/schema.sql)
  :9090 ──▶ Users API    ──▶ PostgreSQL   (postgres-init/schema.sql)
  :7000 ──▶ Orders API   ──▶ MongoDB      (mongodb-init/init.js)
                 └──────────────────────────────────────────────┘
```

## Tech stack

- **ASP.NET Core (.NET 10)**: Minimal APIs and Controllers
- **Entity Framework Core** with MySQL, **Dapper** with PostgreSQL, **MongoDB.Driver**
- **FluentValidation** for request validation in every service
- Global exception-handling middleware
- **Swagger** for every service
- **Docker & Docker Compose**: one network, seeded databases

## Run with Docker

```bash
docker compose up --build
```

Then open Swagger:

- Products: http://localhost:8080/swagger
- Users: http://localhost:9090/swagger
- Orders: http://localhost:7000/swagger

> The databases are exposed on `3307` (MySQL) and `5433` (Postgres) so they don't clash with local installs.

## Endpoints

### Products: `/api/products`
| Method | Route | Description |
|---|---|---|
| GET | `/api/products` | List all products |
| GET | `/api/products/{productID}` | Get one product |
| POST | `/api/products` | Create a product |
| PUT | `/api/products` | Update a product |
| DELETE | `/api/products/{productID}` | Delete a product |

### Users: `/api/auth`, `/api/users`
| Method | Route | Description |
|---|---|---|
| POST | `/api/auth/register` | Register a new user |
| POST | `/api/auth/login` | Log in |
| GET | `/api/users/{userID}` | Get a user by id |

A Postman collection is included: `eCommerceSolution.UsersService/eCommerce.Users.postman_collection.json`.

### Orders: `/api/orders`
| Method | Route | Description |
|---|---|---|
| GET | `/api/orders` | List all orders |
| GET | `/api/orders/search/orderid/{orderID}` | Find by order id |
| GET | `/api/orders/search/productid/{productID}` | Orders containing a product |
| GET | `/api/orders/search/orderDate/{orderDate}` | Orders on a date |
| GET | `/api/orders/search/userid/{userID}` | Orders of a user |
| POST | `/api/orders` | Create an order |
| PUT | `/api/orders/{orderID}` | Update an order |
| DELETE | `/api/orders/{orderID}` | Delete an order |

## Project structure

```
├── docker-compose.yaml
├── eCommerceSolution.ProductsService/
│   ├── ProductsMicroService.API/   # Minimal API endpoints
│   ├── BusinessLogicLayer/         # services, DTOs, validators
│   ├── DataAccessLayer/            # EF Core DbContext, repositories
│   └── mysql-init/                 # schema + seed data
├── eCommerceSolution.UsersService/
│   ├── eCommerce.API/              # controllers
│   ├── Core/                       # entities, DTOs, validators, contracts
│   ├── eCommerce.Infrastructure/   # Dapper repositories
│   └── postgres-init/              # schema
└── eCommerceSolution.OrderService/
    ├── OrdersMicroservice.API/     # controllers, middleware
    ├── BusinessLogicLayer/         # services, mappers, validators
    ├── DataAccessLayer/            # MongoDB repositories
    └── mongodb-init/               # seed data
```

## Run a single service locally

Each service has its own solution file, so you can run it without Docker (you need the matching database running):

```bash
dotnet run --project eCommerceSolution.ProductsService/ProductsMicroService.API
```

> The passwords in `docker-compose.yaml` (`1111`) are for local development only.
