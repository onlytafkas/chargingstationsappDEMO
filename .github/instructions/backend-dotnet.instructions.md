---
description: Architecture and implementation rules for the ASP.NET Core backend.
applyTo: backend/**
---

# Backend Architecture

- The backend is an ASP.NET Core (.NET 8) Web API.
- The backend is the ONLY layer that accesses the database.
- Entity Framework Core is used for data access.
- The database is MySQL.
- Backend code MUST be structured as `Controllers -> Services -> Repositories`.

## Layering rules
- Controllers live in `backend/Controllers` and handle HTTP concerns only.
- Services live in `backend/Services` and contain business/application logic.
- Repositories live in `backend/Repositories` and contain database access logic.
- Controllers MUST depend on service interfaces, not on `DbContext`, repositories, or concrete service implementations directly.
- Service interfaces SHOULD live alongside the service layer and be registered through dependency injection.
- Services MUST depend on repository interfaces or other service abstractions, not on concrete repository implementations.
- Repository interfaces SHOULD live alongside the repository layer and be registered through dependency injection.
- Services MUST NOT depend on ASP.NET MVC types such as `ControllerBase`, `ActionResult`, or `HttpContext`.
- Repositories are the only layer that should execute Entity Framework queries and commands.
- When adding backend features, create or update all affected layers rather than collapsing logic into controllers.

## Configuration rules
- For local development, MySQL connection settings MUST be stored in a gitignored `backend/.env` file.
- Backend startup/configuration code MUST load `backend/.env` before resolving MySQL connection settings.
- Backend `.env` loading MUST preserve raw secret values exactly as written. Do NOT use loaders or parsing modes that interpolate `$` or otherwise rewrite passwords and connection secrets.
- When editing `Program.cs` or any configuration/bootstrap code, preserve or add `.env` loading rather than bypassing it.
- For local backend database work, treat `backend/.env` as the primary source of connection details.
- Never commit `backend/.env` or any other file containing secrets.

## API rules
- Controllers expose REST endpoints only.
- The backend enforces authentication and authorization.
- The backend validates all input.
- The backend ensures users can only access their own data.
- Remove template/sample API artifacts such as `WeatherForecast` endpoints, models, and placeholder handlers once real backend features exist.
- `backend/backend.http` MUST contain runnable requests for the real backend endpoints only.
- Whenever a backend endpoint is added, removed, or changed, update `backend/backend.http` in the same change so its requests, headers, payloads, and URLs stay aligned with the live API.

The frontend must never bypass the backend.
