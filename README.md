# HR Management Application

A modern HR Management System built with ASP.NET Core 8.0 MVC, featuring employee management capabilities and XML data import functionality.

## Features

- 👥 Complete Employee Management (CRUD operations)
- 📊 Responsive data display using Bootstrap
- 📱 Mobile-friendly interface
- 📤 XML data import capability
- 🔐 Input validation
- 🎯 Clean Architecture
- 🛠 Entity Framework Core for data management
- 🐳 Docker support for containerized deployment

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (recommended) or VS Code
- SQL Server (LocalDB is included with Visual Studio)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (for containerized deployment)

## Project Structure

```
HRMWeb/
├── Controllers/
│   └── EmployeesController.cs
├── Models/
│   └── Employee.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Services/
│   ├── IEmployeeService.cs
│   └── EmployeeService.cs
├── Views/
│   ├── Employees/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   ├── Details.cshtml
│   │   └── Delete.cshtml
│   └── Shared/
│       └── _Layout.cshtml
└── wwwroot/
```

## Installation & Setup

### Option 1: Traditional Setup

1. Clone the repository
```bash
git clone https://github.com/zawad1992/emp-management.git
cd HRMWeb
```

2. Install the Entity Framework Core tools globally
```bash
dotnet tool install --global dotnet-ef
```

3. Install required NuGet packages
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
```

4. Update the connection string in `appsettings.json` if needed
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HRMWeb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

5. Create and apply database migrations
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Option 2: Docker Deployment

1. Clone the repository
```bash
git clone https://github.com/zawad1992/emp-management.git
cd HRMWeb
```

2. Build and run using Docker Compose
```bash
docker-compose up -d
```

The application will be available at `http://localhost:8080`

## Running the Application

1. Using Visual Studio:
   - Open the solution file (.sln)
   - Press F5 to run the application
   - The application will open in your default browser

2. Using command line:
```bash
dotnet run
```

3. Using Docker:
```bash
# Build and start the containers
docker-compose up -d

# Check if containers are running
docker ps

# Access the application
Open http://localhost:8080 in your browser

# View application logs
docker-compose logs -f webapp

# Stop the application
docker-compose down
```

## Technical Documentation

### Database Schema

The Employee table contains the following fields:

| Field      | Type         | Constraints |
|------------|--------------|-------------|
| EmployeeId | int          | Primary Key, Auto-increment |
| FirstName  | nvarchar(50) | Required |
| LastName   | nvarchar(50) | Required |
| Division   | nvarchar(50) | Required |
| Building   | nvarchar(50) | Required |
| Title      | nvarchar(50) | Required |
| Room       | nvarchar(20) | Required |

### Docker Configuration

1. `Dockerfile` - Defines the application container:
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["HRMWeb.csproj", "./"]
RUN dotnet restore
COPY . .
RUN dotnet build "HRMWeb.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "HRMWeb.csproj" -c Release -o /app/publish /p:UseAppHost=false
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Docker
EXPOSE 80
ENTRYPOINT ["dotnet", "HRMWeb.dll"]
```

2. `docker-compose.yml` - Orchestrates the application and database containers:
```yaml
version: '3.8'
services:
  db:
    container_name: hrmweb-db
    image: mcr.microsoft.com/mssql/server:2022-latest
    ports:
      - "1433:1433"
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourStrong@Passw0rd
      - MSSQL_PID=Express
    volumes:
      - sqldata:/var/opt/mssql
    networks:
      - hrmweb-network

  webapp:
    container_name: hrmweb-webapp
    build:
      context: .
      dockerfile: Dockerfile
    ports:
      - "8080:80"
    depends_on:
      - db
    environment:
      - ASPNETCORE_ENVIRONMENT=Docker
      - ConnectionStrings__DefaultConnection=Server=db;Database=HRMWeb;User=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True
    networks:
      - hrmweb-network

volumes:
  sqldata:
    name: hrmweb_sqldata

networks:
  hrmweb-network:
    name: hrmweb_network
    driver: bridge
```

### Docker Commands

Common Docker commands for managing the application:

```bash
# Start the application
docker-compose up -d

# View logs
docker-compose logs -f

# Stop the application
docker-compose down

# Rebuild and restart
docker-compose up -d --build

# Remove all containers and volumes
docker-compose down -v
```

## Features Detail

### Employee Management
- View all employees in a paginated list
- Add new employees
- Edit existing employee information
- View detailed employee information
- Delete employees from the system

### XML Data Import

The application supports importing employee data via XML. Here's a sample XML structure:

```xml
<?xml version="1.0" encoding="utf-8"?>
<Employees>
    <Employee>
        <FirstName>John</FirstName>
        <LastName>Doe</LastName>
        <Division>Information Technology</Division>
        <Building>Tech Tower</Building>
        <Title>Senior Software Engineer</Title>
        <Room>T-501</Room>
    </Employee>
</Employees>
```

To import data:
1. Prepare your XML file following the above structure
2. Go to the main employees page
3. Click the "Choose File" button
4. Select your XML file
5. Click "Import XML"
6. You have a sample file SampleEmployees.xml in Project Root Directory.

### User Interface
- Responsive Bootstrap-based design
- Clean and intuitive navigation
- Form validation
- Success/Error notifications
- Mobile-friendly layout

## Best Practices Implemented

- Repository Pattern
- Dependency Injection
- Async/Await Pattern
- SOLID Principles
- Clean Architecture
- Proper Exception Handling
- Input Validation
- Responsive Design

## Troubleshooting

### Database Connection Issues
- Verify connection string in appsettings.json
- Ensure SQL Server is running
- Check if migrations are applied

### XML Import Issues
- Verify XML follows the correct structure
- Check for special characters
- Ensure all required fields are present

### Entity Framework Issues
- Update packages to latest version
- Rebuild solution
- Delete migrations folder and recreate if needed

### Docker Deployment Issues
- Ensure Docker network is created properly
- Check if SQL Server container is healthy
- Verify connection string in environment variables
- Check container logs: `docker-compose logs`
- Ensure ports are not in use by other applications
- Verify Docker Desktop is running

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## Acknowledgments

- Built with ASP.NET Core 8.0
- Uses Bootstrap for UI
- Entity Framework Core for data access
- SQL Server for data storage

## Support

For support, please open an issue in the repository or contact [zawad1992@gmail.com]