# Vertical Slice Template

A .NET 10 API template built around **vertical slice architecture** and orchestrated
with **.NET Aspire**. The Aspire AppHost wires the API together with its supporting
infrastructure — SQL Server, Redis, and Seq — so you can run the whole system locally
with a single command.

## Solution structure

| Project | Description |
| --- | --- |
| `src/VerticalSliceTemplate.AppHost` | Aspire orchestrator and local run entry point. Provisions SQL Server, Redis and Seq containers and launches the API. |
| `src/VerticalSliceTemplate.Api` | ASP.NET Core API organized into vertical slices, with API documentation exposed via Scalar. |
| `src/VerticalSliceTemplate.ServiceDefaults` | Shared Aspire service defaults (telemetry, health checks, resilience). |
| `tests/VerticalSliceTemplate.Api.Tests` | Unit tests. |
| `tests/VerticalSliceTemplate.Api.Integration.Tests` | Integration tests. |

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- A running container runtime — [Docker Desktop](https://www.docker.com/products/docker-desktop/)
  or a compatible alternative. The AppHost starts SQL Server, Redis, and Seq as
  containers, so the runtime must be running before you start the app.

## First-time setup

Trust the ASP.NET Core development HTTPS certificate (required for the `https` profile):

```bash
dotnet dev-certs https --trust
```

> The SQL Server password is supplied by the `sqlPassword` parameter, which already has
> a default value in `src/VerticalSliceTemplate.AppHost/appsettings.json`. No additional
> configuration is required to run locally. If you want to override it, set a user secret
> on the AppHost project:
>
> ```bash
> dotnet user-secrets --project src/VerticalSliceTemplate.AppHost set "Parameters:sqlPassword" "<your-password>"
> ```

## Running the solution with Aspire (https profile)

From the repository root, run the AppHost using the `https` launch profile:

```bash
dotnet run --project src/VerticalSliceTemplate.AppHost --launch-profile https
```

This will:

1. Start the SQL Server, Redis, and Seq containers.
2. Start the API.
3. Open the **Aspire dashboard** at <https://localhost:17178>.

From the dashboard you can view each resource, its logs, and its endpoints. Open the
API's **Scalar UI** link (served at `/scalar` on the API's HTTPS endpoint) to explore and
call the API.

> **Using an IDE?** Set `VerticalSliceTemplate.AppHost` as the startup project, select the
> **https** launch profile, and start debugging — this is equivalent to the command above.

## Running tests

```bash
dotnet test
```

## Troubleshooting

- **Containers won't start / connection errors:** ensure Docker Desktop (or your container
  runtime) is running before launching the AppHost.
- **HTTPS/certificate warnings:** run `dotnet dev-certs https --trust` and restart your browser.
- **Persistent containers:** SQL Server, Redis, and Seq are configured with a persistent
  lifetime, so their data survives restarts. Remove the containers manually if you need a
  clean state.
- **Ports in use:** the local endpoints use fixed ports (dashboard `17178`, Seq `8002`,
  SQL Server `8003`, Redis `8004`). Free these ports or update the configuration if they
  conflict with other services.
