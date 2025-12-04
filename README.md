# Vertical Slice Architecture

This repository provides a sample solution implementing **Vertical Slice Architecture** using **.NET 10**, **ASP.NET Core Minimal APIs**, **Entity Framework Core**, and **Sql Server**.  
It is fully prepared for both local development and containerized deployment using **Docker**.

## 🧱 Project Structure

```
VerticalSliceArchitecture/
├── Web.Api/                      # Main API project
│   ├── Features/                # Vertical slices (e.g., Customers)
│   ├── Database/                # EF Core context and migrations
│   ├── Entities/                # Domain entities
│   ├── Extensions/              # Extension methods and configuration helpers
│   ├── Middlewares/             # Custom middlewares and exception handlers
│   ├── Program.cs               # Primary entry point
│   ├── DependencyInjection.cs   # Service registrations
│   └── appsettings*.json        # Environment-specific configuration
├── docker-compose.yml           # Container orchestration
├── docker-compose.override.yml  # Additional development overrides
├── docker-compose.dcproj        # Docker Compose project (Visual Studio)
├── Directory.Build.props        # Central build configuration
├── Directory.Packages.props     # Centralized NuGet package version management
├── VerticalSliceArchitecture.sln # Visual Studio solution file
└── .containers/pgdata/          # PostgreSQL data persistence
```

## 🚀 Technologies Used

- .NET 10
- ASP.NET Core Minimal APIs
- Entity Framework Core
- Sql Server
- FluentValidation
- Docker & Docker Compose
- Swagger / OpenAPI

## 🧩 Vertical Slice Architecture Overview

Each slice represents a **complete vertical feature**, grouping:

- The endpoint(s)
- Validation logic
- Business logic

Example:

```
Features/Customers/
├── CreateCustomer.cs
├── GetCustomer.cs
```

## 🛠 Database Configuration

`Web.Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "Database": "Host=local.postgres;Database=storedb;Username=postgres;Password=postgres"
  }
}
```

## ▶️ Running the Application Locally

### Option 1 — Docker

```bash
docker compose up --build
```

### Option 2 — Local execution

```bash
dotnet run --project Web.Api/Web.Api.csproj
```

## 🔗 Endpoints

- POST /customers
- GET /customers/{customerId}

Swagger: http://localhost:5000/swagger

## 🧪 EF Core Commands

```bash
dotnet ef migrations add MigrationName --project Web.Api
dotnet ef database update --project Web.Api
```

## 📄 License

For educational and demonstration purposes.
