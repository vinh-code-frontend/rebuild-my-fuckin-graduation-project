# .NET CLI Cheat Sheet

> Frequently used .NET CLI commands.

---

## 📦 Solution

```bash
# Create a solution
dotnet new sln -n ApiSolution

# Add a project to the solution
dotnet sln add src/Api/Api.csproj

# List all projects in the solution
dotnet sln list
```

---

## 🏗️ Project

```bash
# Create a class library
dotnet new classlib -n App.Api.Domain

# Restore NuGet packages
dotnet restore

# Build the project
dotnet build

# Publish the application
dotnet publish

# Clean build artifacts
dotnet clean

# Generate a new development certificate (optional)
dotnet dev-certs https --clean
dotnet dev-certs https

# Trust the development certificate
dotnet dev-certs https --trust
```

---

## 📚 NuGet Packages

```bash
# Install a package
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

# Remove a package
dotnet remove package Microsoft.EntityFrameworkCore.SqlServer

# List installed packages
dotnet list package
```

---

## 🗄️ Entity Framework Core

```bash
# Create a migration
dotnet ef migrations add AddUserTable

# Apply all pending migrations
dotnet ef database update

# Apply a specific migration
dotnet ef database update AddUserTable

# Roll back to a previous migration
dotnet ef database update PreviousMigration

# Remove the latest migration
dotnet ef migrations remove

# List all migrations
dotnet ef migrations list
```
