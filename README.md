# TaskFlow API

Backend API for managing projects and tasks. Built with .NET 9, Clean Architecture, and CQRS.

## Setup

1. Make sure you have **.NET 9 SDK** and **SQL Server** installed
2. Update the connection string in `TaskFlow.Api/appsettings.json` if needed
3. Run the app:

```bash
dotnet run --project TaskFlow.Api
```

The database gets created and seeded automatically on first run.

Swagger: http://localhost:5027/swagger

## Default login

- **Email:** admin@taskflow.com
- **Password:** Admin@123

## Endpoints

Check Swagger for full details. Main routes:

- `POST /api/auth/register` - register
- `POST /api/auth/login` - login (returns JWT + refresh token)
- `POST /api/auth/refresh` - refresh token
- `POST /api/auth/logout` - logout (requires auth)
- `GET /api/auth/me` - current user (requires auth)
- `GET/POST/PUT/DELETE /api/admin/projects` - project CRUD (requires auth)
- `GET/POST/PUT/DELETE /api/admin/tasks` - task CRUD (requires auth)
- `PUT /api/admin/tasks/{id}/status` - update task status
- `GET /api/admin/tasks/project/{projectId}` - tasks by project

All list endpoints support `?page=1&pageSize=10&search=keyword`.

## Auth

Login returns an access token (30 min) and refresh token (7 days). Pass the access token as `Authorization: Bearer <token>`.

## Tech

- .NET 9 / ASP.NET Core
- Clean Architecture (Domain, Application, Infrastructure, API)
- CQRS with MediatR
- EF Core 9 + SQL Server
- FluentValidation
- AutoMapper
- JWT Authentication
- BCrypt password hashing
