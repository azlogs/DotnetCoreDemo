# Blog Engine - .NET 9.0 Clean Architecture Demo

A sample Blog Engine API built with .NET 9.0 demonstrating Clean Architecture principles, RESTful API design, and modern .NET development practices.

## 🏗️ Architecture

This project follows **Clean Architecture** principles with clear separation of concerns:

```
┌─────────────────────────────────────────────┐
│          Presentation Layer (API)          │
│         BlogEngine.API                      │
│  - Controllers                              │
│  - Startup Configuration                    │
│  - Swagger/OpenAPI                          │
└──────────────────┬──────────────────────────┘
                   │
┌──────────────────┴──────────────────────────┐
│         Application Layer                   │
│      BlogEngine.Services                    │
│  - Business Logic                           │
│  - Service Interfaces & Implementations     │
└──────────────────┬──────────────────────────┘
                   │
┌──────────────────┴──────────────────────────┐
│        Infrastructure Layer                 │
│   BlogEngine.DataRepositories               │
│  - Repository Pattern                       │
│  - Data Access Logic                        │
└──────────────────┬──────────────────────────┘
                   │
┌──────────────────┴──────────────────────────┐
│           Domain Layer                      │
│     BlogEngine.DataModels                   │
│  - Entities (User, Post, Comment)          │
│  - DbContext                                │
│  - Database Migrations                      │
└─────────────────────────────────────────────┘

        Supporting Projects:
        ├── BlogEngine.ViewModels - DTOs
        └── BlogEngine.Utils - Utilities
```

### Layer Responsibilities

- **API Layer** (`BlogEngine.API`): Entry point, HTTP concerns, dependency injection configuration
- **Service Layer** (`BlogEngine.Services`): Business logic, orchestration, application services
- **Repository Layer** (`BlogEngine.DataRepositories`): Data access abstraction, CRUD operations
- **Domain Layer** (`BlogEngine.DataModels`): Core entities, database context, migrations
- **ViewModels** (`BlogEngine.ViewModels`): Data Transfer Objects (DTOs)
- **Utils** (`BlogEngine.Utils`): Shared utilities and helpers

## ✨ Features

- 🔐 **JWT Authentication** - Secure token-based authentication
- 📝 **Blog Post Management** - Create, read, update, delete blog posts
- 💬 **Comment System** - Nested comments with parent-child relationships
- 👤 **User Management** - User registration and profile management
- 📚 **Swagger/OpenAPI** - Interactive API documentation
- 🗄️ **SQLite Database** - Lightweight, zero-configuration database
- 🔄 **EF Core Migrations** - Version-controlled database schema
- 🏗️ **Clean Architecture** - Maintainable and testable code structure
- 🔒 **Security** - Password encryption, JWT tokens, secure endpoints

## 🚀 Quick Start

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- SQL Server (or use Docker Compose - see below)
- Git

### Docker Compose Setup (Recommended)

The easiest way to get started is using Docker Compose, which includes SQL Server:

```bash
git clone https://github.com/azlogs/DotnetCoreDemo.git
cd DotnetCoreDemo/DemoDotnetCore
docker-compose up -d
```

This will:
- Start SQL Server 2022 in a container
- Wait for SQL Server to be ready
- Start the Blog Engine API
- Create the database automatically

Access the API:
- HTTP: `http://localhost:5000`
- HTTPS: `http://localhost:5001`
- Swagger: `http://localhost:5000/swagger`

To stop:
```bash
docker-compose down
```

To stop and remove database:
```bash
docker-compose down -v
```

### Local Development Setup

If you have SQL Server installed locally:

1. **Clone the repository**
   ```bash
   git clone https://github.com/azlogs/DotnetCoreDemo.git
   cd DotnetCoreDemo/DemoDotnetCore
   ```

2. **Update connection string**
   
   Edit `BlogEngine/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "BlogEngineDatabase": "Server=localhost,1433;Database=BlogEngineDatabase;User Id=sa;Password=YourPassword;TrustServerCertificate=True"
   }
   ```

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Apply database migrations**
   ```bash
   cd BlogEngine.DataModels
   dotnet ef database update
   cd ..
   ```

5. **Run the application**
   ```bash
   cd BlogEngine
   dotnet run
   ```

5. **Access the application**
   
   Navigate to: `https://localhost:5001/swagger` or `http://localhost:5000/swagger`

   The API is now running and you can test all endpoints through the Swagger interface!

## 🔧 Configuration

### App Settings

Copy `appsettings.Example.json` to `appsettings.json` and update the values:

```json
{
  "SecuritySetting": {
    "SecrectKey": "YOUR-SECRET-KEY-HERE-MINIMUM-32-CHARACTERS-LONG",
    "PasswordSalt": "YOUR-PASSWORD-SALT-HERE-GUID-FORMAT"
  },
  "ConnectionStrings": {
    "BlogEngineDatabase": "Server=localhost,1433;Database=BlogEngineDatabase;User Id=sa;Password=YOUR-STRONG-PASSWORD;TrustServerCertificate=True"
  }
}
```

**Security Settings:**
- `SecrectKey`: Used for JWT token signing (min 32 characters)
- `PasswordSalt`: Salt for password hashing (use a GUID)

**Database:**
- SQL Server connection string
- Default uses port 1433
- Database `BlogEngineDatabase` auto-creates on first run

## 📦 Project Structure

```
DemoDotnetCore/
├── BlogEngine/                      # API Project (Presentation)
│   ├── Controllers/                 # API Controllers
│   ├── Options/                     # Configuration options
│   ├── Startup.cs                   # App configuration
│   ├── Program.cs                   # Entry point
│   └── appsettings.json            # Configuration
│
├── BlogEngine.Services/             # Application Services
│   ├── Interfaces/                  # Service contracts
│   └── Implements/                  # Service implementations
│
├── BlogEngine.DataRepositories/     # Data Access Layer
│   ├── Interfaces/                  # Repository contracts
│   └── Implements/                  # Repository implementations
│
├── BlogEngine.DataModels/           # Domain Models
│   ├── Models/                      # Entities & DbContext
│   └── Migrations/                  # EF Core Migrations
│
├── BlogEngine.ViewModels/           # DTOs
│   ├── UserViewModels/
│   ├── PostViewModels/
│   └── CommentViewModels/
│
└── BlogEngine.Utils/                # Utilities
    └── StringCipher.cs              # Encryption utilities
```

## 🗄️ Database Schema

This project uses **SQL Server** as the database.

### Tables

- **User** - User accounts and profiles
- **Post** - Blog posts with title, content, tags
- **Comment** - Comments on posts (supports nesting)
- **Role** - User roles for RBAC
- **UserRole** - Many-to-many relationship between users and roles

### Relationships

- User → Posts (1:many)
- User → Comments (1:many)
- Post → Comments (1:many)
- Comment → Comment (self-referencing for nested comments)

## 🔐 Authentication

This API uses JWT (JSON Web Tokens) for authentication.

1. **Register/Login** to obtain a JWT token
2. **Include the token** in subsequent requests:
   ```
   Authorization: Bearer <your-token>
   ```
3. Swagger UI has built-in JWT authentication support

## 🛠️ Development

### Build the solution
```bash
dotnet build
```

### Run tests (if available)
```bash
dotnet test
```

### Create a new migration
```bash
cd BlogEngine.DataModels
dotnet ef migrations add <MigrationName>
```

### Update database
```bash
cd BlogEngine.DataModels
dotnet ef database update
```

### Rollback migration
```bash
cd BlogEngine.DataModels
dotnet ef database update <PreviousMigrationName>
```

## 📚 API Endpoints

### Users
- `POST /api/users/register` - Register a new user
- `POST /api/users/login` - Login and get JWT token
- `GET /api/users/{id}` - Get user by ID
- `PUT /api/users/{id}` - Update user
- `DELETE /api/users/{id}` - Delete user

### Posts
- `GET /api/posts` - Get all posts
- `GET /api/posts/{id}` - Get post by ID
- `POST /api/posts` - Create new post (requires auth)
- `PUT /api/posts/{id}` - Update post (requires auth)
- `DELETE /api/posts/{id}` - Delete post (requires auth)

### Comments
- `GET /api/comments/post/{postId}` - Get comments for a post
- `POST /api/comments` - Create comment (requires auth)
- `PUT /api/comments/{id}` - Update comment (requires auth)
- `DELETE /api/comments/{id}` - Delete comment (requires auth)

*For detailed request/response schemas, see the Swagger documentation at `/swagger`*

## 🧪 Testing with Swagger

1. Start the application
2. Navigate to `https://localhost:5001/swagger`
3. Click "Authorize" and enter your JWT token
4. Test endpoints directly from the browser

## 🚢 Deployment

### Production Checklist

- [ ] Update `appsettings.json` with production values
- [ ] Use strong, unique `SecrectKey` and `PasswordSalt`
- [ ] Consider using environment variables for secrets
- [ ] Review and harden security settings
- [ ] Set up proper logging and monitoring
- [ ] Configure HTTPS/SSL certificates
- [ ] Set up database backups
- [ ] Review and optimize connection strings

### Docker Deployment

The project includes Docker support with SQL Server for easy containerized deployment.

**Quick Start with Docker Compose:**

```bash
# Navigate to the DemoDotnetCore directory
cd DemoDotnetCore

# Update environment variables in docker-compose.yml
# IMPORTANT: Change the default security keys and SQL Server password!

# Build and run (includes SQL Server 2022)
docker-compose up -d

# View logs
docker-compose logs -f

# Stop containers
docker-compose down

# Stop and remove database volume
docker-compose down -v
```

**What's Included:**
- SQL Server 2022 Developer Edition (free)
- Blog Engine API
- Automatic database initialization
- Health checks to ensure SQL Server is ready

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `http://localhost:5001`
- Swagger: `http://localhost:5000/swagger`
- SQL Server: `localhost:1433`

**Build Docker Image Manually:**

```bash
cd DemoDotnetCore
docker build -t blogengine-api .
docker run -p 5000:8080 -p 5001:8081 blogengine-api
```

**Important Notes:**
- SQL Server data is persisted in a Docker volume
- Default password is `YourStrong@Passw0rd` - **change this for production!**
- Update security keys in `docker-compose.yml` before deployment
- For production, use environment variables or secrets management

## 🔄 Migration from .NET Core 3.1

This project has been upgraded from .NET Core 3.1 to .NET 9.0 with the following improvements:

- ✅ Updated to .NET 9.0 (latest version)
- ✅ Uses SQL Server with Docker Compose for easy setup
- ✅ Added Entity Framework Core migrations
- ✅ Updated all NuGet packages to latest versions
- ✅ Fixed security vulnerabilities
- ✅ Added nullable reference types support
- ✅ Improved Clean Architecture implementation
- ✅ Added Role-Based Access Control (RBAC)
- ✅ Added comprehensive documentation

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is provided as-is for educational and demonstration purposes.

## 🐛 Troubleshooting

### Build Errors
- Ensure you have .NET 9.0 SDK installed: `dotnet --version`
- Run `dotnet restore` to restore packages

### Database Errors
- Ensure SQL Server is running (check `docker-compose ps` if using Docker)
- Check connection string in `appsettings.json`
- Verify SQL Server is accepting connections on port 1433
- For Docker: ensure health check passes with `docker-compose logs sqlserver`

### Authentication Issues
- Ensure `SecrectKey` is at least 32 characters
- Check that Authorization header includes "Bearer " prefix

### Port Conflicts
- Default ports are 5000 (HTTP) and 5001 (HTTPS)
- Change ports in `Properties/launchSettings.json` if needed

## 📧 Support

For issues, questions, or contributions, please open an issue on GitHub.

---

**Built with ❤️ using .NET 9.0 and Clean Architecture principles**
