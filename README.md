# HR Management Application

A modern HR Management System built with ASP.NET Core 8.0 MVC, featuring employee management capabilities and XML data import functionality.

## Features

- 👥 Complete Employee Management (CRUD operations)
- 📊 Responsive data display using Bootstrap
- 📱 Mobile-friendly interface
- 🔍 Search functionality
- 📤 XML data import capability
- 🔐 Input validation
- 🎯 Clean Architecture
- 🛠 Entity Framework Core for data management

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (recommended) or VS Code
- SQL Server (LocalDB is included with Visual Studio)

## Installation

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

## Running the Application

1. Using Visual Studio:
   - Open the solution file (.sln)
   - Press F5 to run the application
   - The application will open in your default browser

2. Using command line:
```bash
dotnet run
```

## XML Data Import

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

## Database Schema

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

## Features Detail

### Employee Management
- View all employees in a paginated list
- Add new employees
- Edit existing employee information
- View detailed employee information
- Delete employees from the system

### Data Import
- Import multiple employee records via XML
- Validation of XML structure
- Bulk import capability
- Error handling and reporting

### User Interface
- Responsive Bootstrap-based design
- Clean and intuitive navigation
- Form validation
- Success/Error notifications
- Mobile-friendly layout

## Error Handling

The application includes comprehensive error handling:
- Client-side validation
- Server-side validation
- Database exception handling
- XML import validation
- User-friendly error messages

## Best Practices Implemented

- Repository Pattern
- Dependency Injection
- Async/Await Pattern
- SOLID Principles
- Clean Architecture
- Proper Exception Handling
- Input Validation
- Responsive Design

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## Common Issues and Solutions

1. **Database Connection Issues**
   - Verify connection string in appsettings.json
   - Ensure SQL Server is running
   - Check if migrations are applied

2. **XML Import Issues**
   - Verify XML follows the correct structure
   - Check for special characters
   - Ensure all required fields are present

3. **Entity Framework Issues**
   - Update packages to latest version
   - Rebuild solution
   - Delete migrations folder and recreate if needed


## Acknowledgments

- Built with ASP.NET Core 8.0
- Uses Bootstrap for UI
- Entity Framework Core for data access
- SQL Server for data storage

## Support

For support, please open an issue in the repository or contact [zawad1992@gmail.com]
