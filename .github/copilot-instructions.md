# Project Overview

This is an e-commerce platform for selling mobile phones.

## Technology

Backend:

- ASP.NET Core
- .NET 10
- Entity Framework Core
- SQL Server

Admin:

- React
- TypeScript
- Vite

Client:

- Nuxt
- Vue
- TypeScript

## Architecture

Backend follows Clean Architecture.

Domain
↓
Application
↓
Infrastructure
↓
API

Domain must not depend on Infrastructure.

Application must not directly depend on EF Core implementation details.

## Backend Rules

Use:

- DTOs for API contracts
- Entities for persistence/domain
- Repository pattern where appropriate
- CancellationToken for async I/O operations
- Async APIs
- Fluent validation / appropriate validation
- Centralized exception handling

## Database

Use SQL Server.

Do not rely solely on application-level uniqueness checks.

Important business uniqueness constraints must also
be enforced at the database level.

## API

Use lowercase URLs.

Use appropriate HTTP status codes.

Never expose database entities directly from controllers.

## General Rules

Before implementing a feature:

1. Understand existing architecture.
2. Search for existing patterns.
3. Reuse existing abstractions.
4. Avoid introducing unnecessary abstractions.
5. Do not modify unrelated code.

Never rewrite working code without a concrete reason.
