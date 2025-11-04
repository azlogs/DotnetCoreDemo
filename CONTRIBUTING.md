# Contributing to Blog Engine

Thank you for your interest in contributing to the Blog Engine project! This document provides guidelines and information for contributors.

## 🏗️ Architecture Overview

This project follows **Clean Architecture** principles with the following layers:

### 1. Domain Layer (`BlogEngine.DataModels`)
- Contains core entities and business models
- Database context and EF Core configuration
- Database migrations
- **No dependencies on other layers**

### 2. Repository Layer (`BlogEngine.DataRepositories`)
- Implements the Repository pattern
- Data access logic and CRUD operations
- Depends only on the Domain layer

### 3. Service Layer (`BlogEngine.Services`)
- Business logic and application services
- Orchestrates data from repositories
- Depends on Repository and Domain layers

### 4. Presentation Layer (`BlogEngine.API`)
- REST API controllers
- Dependency injection configuration
- Swagger/OpenAPI documentation
- Depends on Service layer

### Supporting Projects
- **BlogEngine.ViewModels**: DTOs and view models for API requests/responses
- **BlogEngine.Utils**: Shared utilities and helper functions

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK or later
- Git
- Your favorite IDE (Visual Studio, VS Code, Rider)

### Setup
1. Fork the repository
2. Clone your fork
3. Restore dependencies: `dotnet restore`
4. Apply migrations: `cd BlogEngine.DataModels && dotnet ef database update`
5. Run the application: `cd ../BlogEngine && dotnet run`

## 📝 Development Guidelines

### Code Style
- Follow C# coding conventions
- Use meaningful variable and method names
- Add XML documentation for public APIs
- Keep methods small and focused

### Testing
- Write unit tests for new features
- Ensure existing tests pass before submitting PR
- Aim for good test coverage

### Commits
- Write clear, descriptive commit messages
- Use conventional commit format: `type(scope): message`
  - `feat`: New feature
  - `fix`: Bug fix
  - `docs`: Documentation changes
  - `refactor`: Code refactoring
  - `test`: Adding tests
  - `chore`: Maintenance tasks

### Pull Requests
1. Create a feature branch: `git checkout -b feature/amazing-feature`
2. Make your changes
3. Test thoroughly
4. Commit your changes
5. Push to your fork
6. Create a Pull Request

## 🔧 Project Structure

```
DemoDotnetCore/
├── BlogEngine/                  # API Layer
│   ├── Controllers/            # API endpoints
│   ├── Options/                # Configuration classes
│   └── Startup.cs              # DI & middleware config
│
├── BlogEngine.Services/         # Business Logic
│   ├── Interfaces/             # Service contracts
│   └── Implements/             # Service implementations
│
├── BlogEngine.DataRepositories/ # Data Access
│   ├── Interfaces/             # Repository contracts
│   └── Implements/             # Repository implementations
│
├── BlogEngine.DataModels/       # Domain Models
│   ├── Models/                 # Entities & DbContext
│   └── Migrations/             # EF Core Migrations
│
├── BlogEngine.ViewModels/       # DTOs
└── BlogEngine.Utils/            # Utilities
```

## 🗄️ Database Migrations

### Creating a Migration
```bash
cd BlogEngine.DataModels
dotnet ef migrations add YourMigrationName
```

### Applying Migrations
```bash
dotnet ef database update
```

### Rollback Migration
```bash
dotnet ef database update PreviousMigrationName
```

## 🔐 Security

- Never commit secrets or API keys
- Use appsettings.Example.json for templates
- Follow OWASP security guidelines
- Report security vulnerabilities privately

## 📚 Adding New Features

### Adding a New Entity
1. Create entity in `BlogEngine.DataModels/Models/`
2. Add DbSet to `BlogEngineDatabaseContext`
3. Configure entity in `OnModelCreating`
4. Create migration
5. Create repository interface and implementation
6. Create service interface and implementation
7. Register services in `Startup.cs`
8. Create view models
9. Add controller endpoints

### Adding a New API Endpoint
1. Create view models in `BlogEngine.ViewModels/`
2. Add service method if needed
3. Add controller action in appropriate controller
4. Test with Swagger
5. Update API documentation

## 🧪 Testing Guidelines

### Unit Tests
- Test business logic in isolation
- Mock dependencies
- Use AAA pattern (Arrange, Act, Assert)

### Integration Tests
- Test API endpoints
- Use test database
- Clean up after tests

## 📖 Documentation

- Update README.md for user-facing changes
- Update this file for contributor changes
- Add XML comments for public APIs
- Update Swagger annotations

## ❓ Questions?

- Open an issue for bugs
- Start a discussion for feature ideas
- Check existing issues before creating new ones

## 📄 License

By contributing, you agree that your contributions will be licensed under the same license as the project.

---

Thank you for contributing to Blog Engine! 🎉
