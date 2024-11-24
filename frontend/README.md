# Kanban Application

A full-stack Kanban application using Vue.js, C# with Hexagonal Architecture and CQRS/Event Sourcing, and SQL Server.

## Project Structure

```
├── frontend/               # Vue.js frontend application
├── backend/               # C# backend application
│   ├── Kanban.Api/       # API Layer
│   ├── Kanban.Application/  # Application Layer
│   ├── Kanban.Domain/    # Domain Layer
│   └── Kanban.Infrastructure/  # Infrastructure Layer
└── docker-compose.yml    # Docker composition file
```

## Requirements

- Docker Desktop
- .NET SDK 7.0
- Node.js 18+

## Getting Started

1. Clone the repository
2. Run `docker-compose up --build`
3. Access the application at http://localhost:8080

## Architecture

The backend follows Hexagonal Architecture with CQRS and Event Sourcing:

- Domain Layer: Contains business logic and domain models
- Application Layer: Contains application services, commands, and queries
- Infrastructure Layer: Contains implementations of repositories and external services
- API Layer: Contains REST API endpoints and controllers

## Features

- Create new tasks
- Change task status (Todo, InProgress, Done)
- Archive completed tasks