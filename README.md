# .NET Core Clean Architecture Starter

A small **.NET Core Web API** starter template that demonstrates clean architecture with a simple, easy-to-follow structure.

The sample uses a basic todos feature to keep the code easy to follow. The main point is the project structure: domain rules, application use cases, infrastructure, and HTTP concerns are kept in separate layers.

## Architecture

```text
WebApi ───────────────┐
                      v
Infrastructure ──> Application ──> Domain
```

The dependency direction is:

- `Domain` has no dependency on any other project.
- `Application` depends on `Domain` and defines use cases plus repository contracts.
- `Infrastructure` implements application contracts, currently with an in-memory repository.
- `WebApi` handles HTTP concerns and delegates work to application services.

## Project Layout

```text
dotnet-core-clean-architecture-starter/
├─ Dockerfile
├─ README.md
├─ dotnet-core-clean-architecture-starter.sln
└─ src/
   ├─ Domain/
   │  ├─ Common/
   │  │  └─ DomainException.cs
   │  └─ Entities/
   │     └─ TodoItem.cs
   ├─ Application/
   │  ├─ Common/
   │  │  └─ Result.cs
   │  └─ Todos/
   │     ├─ ITodoRepository.cs
   │     ├─ ITodoService.cs
   │     ├─ TodoService.cs
   │     └─ *.cs command and DTO contracts
   ├─ Infrastructure/
   │  └─ Persistence/
   │     └─ InMemoryTodoRepository.cs
   └─ WebApi/
      ├─ Controllers/
      │  ├─ HealthController.cs
      │  └─ TodosController.cs
      ├─ Program.cs
      └─ WebApi.csproj
```

## What It Shows

- `Domain` contains entity behavior and validation.
- `Application` contains use cases and does not reference ASP.NET Core.
- `Infrastructure` contains the repository implementation.
- `WebApi` contains controllers, Swagger, and dependency wiring.
- The current persistence layer is in-memory so the example stays small.
- Docker is included for a simple container build.

## Run Locally

```bash
dotnet restore
dotnet run --project src/WebApi/WebApi.csproj
```

Then open:

- API: `http://localhost:5000`
- Swagger UI: `http://localhost:5000/swagger`

If your machine uses a different ASP.NET Core URL, use the URL printed by `dotnet run`.

## Run with Docker

```bash
docker build -t dotnet-core-clean-architecture-starter .
docker run -d -p 8080:8080 --name dotnet-core-api dotnet-core-clean-architecture-starter
```

Inside the container the app binds to `http://+:8080`, so use `http://localhost:8080/swagger`.

## Endpoints

- `GET /api/health` returns status and UTC timestamp.
- `GET /api/todos` lists todos.
- `GET /api/todos/{id}` fetches one todo.
- `POST /api/todos` creates a todo.
- `PUT /api/todos/{id}` updates title and/or completion.
- `DELETE /api/todos/{id}` removes a todo.

Example create request:

```json
{
  "title": "Write an architecture decision record"
}
```

## Notes

The in-memory repository is just a placeholder. To add a database, create another `ITodoRepository` implementation in `Infrastructure` and update the dependency registration.
