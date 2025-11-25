
# Vertical Slice Architecture — .NET 9 Minimal API Sample

This repository is a learning and starter project that demonstrates how to implement **Vertical Slice Architecture (VSA)** using **.NET 9**, **ASP.NET Core Minimal APIs**, **Entity Framework Core**, and **PostgreSQL**. It is ready for **local development** and **container-based deployment** with **Docker Compose**.

---

## Solution Structure

```text
VerticalSliceArchitecture/
├── Web.Api/                      # Main API project
│   ├── Features/                 # Vertical slices (e.g., Customers)
│   ├── Database/                 # EF Core DbContext and migrations
│   ├── Entities/                 # Domain entities / aggregates
│   ├── Extensions/               # DI, configuration, and helper extensions
│   ├── Middlewares/              # Custom middleware & exception handling
│   ├── Program.cs                # Application entry point
│   ├── DependencyInjection.cs    # Service registration
│   └── appsettings*.json         # Environment-specific configuration
├── docker-compose.yml            # Container orchestration for API + DB
├── docker-compose.override.yml   # Extra development-time configuration
├── docker-compose.dcproj         # Visual Studio Docker Compose project
├── Directory.Build.props         # Global build configuration
├── Directory.Packages.props      # Central NuGet package management
├── VerticalSliceArchitecture.sln # Visual Studio solution
└── .containers/pgdata/           # PostgreSQL data persistence (local)
```

---

## Technology Stack

This sample uses a modern, production-inspired stack:

- **.NET 9** — latest .NET runtime and SDK for high-performance APIs  
- **ASP.NET Core Minimal APIs** — lightweight HTTP endpoints without controllers  
- **Entity Framework Core** — data access and migrations  
- **PostgreSQL** — relational database engine  
- **FluentValidation** — input validation and rich error messages  
- **Docker & Docker Compose** — local development and container orchestration  
- **Swagger / OpenAPI** — interactive API documentation

---

## Vertical Slice Architecture in a Nutshell

Instead of organizing the codebase by layers (Controllers, Services, Repositories), **Vertical Slice Architecture** organizes by **feature**.

Each **slice** encapsulates everything required for a specific use case:

- HTTP endpoint(s)
- Request/response DTOs
- Validation logic
- Application / domain logic
- Data access required for that feature

Example structure under `Features/Customers`:

```text
Features/Customers/
├── CreateCustomer.cs   # Endpoint + handler + validation for creating customers
├── GetCustomer.cs      # Endpoint + handler for querying a single customer
```

This approach:

- Keeps features **cohesive** and **self-contained**
- Minimizes cross-feature coupling
- Makes it easier to reason about a single behavior end-to-end
- Scales well as you add more use cases over time

---

## Database Configuration

The PostgreSQL connection string is defined in `Web.Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "Database": "Host=local.postgres;Database=storedb;Username=postgres;Password=postgres"
  }
}
```

> ⚠️ **Do not** use plain-text credentials in production.  
> Use environment variables, a secrets manager (e.g., Azure Key Vault), or Docker secrets.

In development, EF Core migrations are automatically applied at startup, so your local database is kept in sync with the current model.

---

## Running the Project Locally

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/) installed
- [Docker Desktop](https://www.docker.com/) or compatible Docker engine

---

### Option 1 — Run Everything with Docker (API + PostgreSQL)

From the repository root:

```bash
docker compose up --build
```

This will start:

- **API** at:
  - http://localhost:5000
  - https://localhost:5001
- **PostgreSQL** at:
  - `localhost:5432` (service name `local.postgres` inside Docker network)

To stop the environment:

```bash
docker compose down
```

If you want to remove volumes as well (including database data):

```bash
docker compose down -v
```

---

### Option 2 — Run the API Directly (without Docker for the API)

1. Make sure PostgreSQL is running locally. You can:
   - Use the `local.postgres` container from `docker-compose.yml`, or  
   - Use your own PostgreSQL instance with a compatible connection string.

2. Update `appsettings.Development.json` if necessary to point to your database.

3. Run the API project:

```bash
dotnet run --project Web.Api/Web.Api.csproj
```

The API will be available at the URLs printed in the console (typically `http://localhost:5000` and `https://localhost:5001`).

---

## Sample Endpoints

Once the API is running, you can test the base feature:

- `POST /customers` — create a new customer
- `GET /customers/{customerId}` — retrieve a customer by ID

Interactive API documentation:

- Swagger UI: **http://localhost:5000/swagger**

Use the Swagger UI to explore and test endpoints without needing external tools.

---

## Entity Framework Core Migrations

This project uses EF Core migrations for schema evolution.

### Create a New Migration

From the repository root (or any folder where `dotnet ef` is available):

```bash
dotnet ef migrations add <MigrationName> --project Web.Api
```

Example:

```bash
dotnet ef migrations add AddCustomerBirthdate --project Web.Api
```

### Apply Migrations Manually

If you want to apply migrations yourself instead of at startup:

```bash
dotnet ef database update --project Web.Api
```

---

##  Development Notes

- Validation is handled via **FluentValidation** and translated into standardized `ProblemDetails` responses.
- Middleware is used to centralize error handling and logging for a consistent API surface.
- Adding a new feature is as simple as creating a new folder in `Features/` and defining:
  - The endpoint(s)
  - The handler / business logic
  - The request/response models
  - The validator (if needed)

This design keeps your codebase modular and makes onboarding new developers easier—they can understand a feature by looking at a single slice instead of chasing logic across multiple layers.

---

## License & Usage

This repository is intended for **educational and demonstration purposes**.  
Before using it in production, make sure to:

- Harden security (authentication, authorization, secrets management)
- Add robust logging, telemetry, and observability
- Review performance, scalability, and deployment requirements for your environment

You are free to adapt the code to your own projects as a starting point or reference implementation.

---

Happy coding, and enjoy exploring Vertical Slice Architecture with .NET 9! 🚀
