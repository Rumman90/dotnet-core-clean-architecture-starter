# .NET 8 Web API Starter (Swagger Ready)

Seed project for a simple controller-based **.NET 8 Web API** with health and in-memory todos endpoints.

## Features

- Controller-first setup (no minimal API magic)
- Health check (`/api/health`) plus CRUD todos (`/api/todos`)
- Swagger/OpenAPI always on
- Ready-to-use Dockerfile (exposes `8080`)
- Clean `.gitignore` and small, readable codebase

---

## Project Layout

```text
dotnetcore-starter-template/
├─ Dockerfile
├─ README.md
├─ dotnetcore-starter-template.sln
└─ src/
   └─ WebApi/
      ├─ Controllers/
      │  ├─ HealthController.cs
      │  └─ TodosController.cs
      ├─ Models/
      │  └─ Todo.cs
      ├─ Program.cs
      ├─ WebApi.csproj
      ├─ appsettings.json
      └─ appsettings.Development.json
```

---

## Run Locally

```bash
dotnet restore
dotnet run --project src/WebApi/WebApi.csproj
```

The API listens on `http://localhost:5000` (set via `UseUrls`). Swagger UI: `http://localhost:5000/swagger`.

---

## Run with Docker

```bash
docker build -t dotnetcore-starter-template .
docker run -d -p 8080:8080 --name dotnetcore-api dotnetcore-starter-template
```

Inside the container the app binds to `http://+:8080`, so you'll hit it at `http://localhost:8080`.

---

## Endpoints

- `GET /api/health` — status + UTC timestamp
- `GET /api/todos` — list all todos
- `GET /api/todos/{id}` — fetch by id
- `POST /api/todos` — create (`{ "title": "Buy milk" }`)
- `PUT /api/todos/{id}` — update title/completed
- `DELETE /api/todos/{id}` — remove

Happy coding!
