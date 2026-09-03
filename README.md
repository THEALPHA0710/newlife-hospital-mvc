# NewLife Hospital - Membership Module

## Project layout
- Models/PatientInfoDetail.cs   - MVC/EF model with data annotations
- Models/PatientInfoDbContext.cs - DbContext (OnConfiguring holds the connection string)
- Models/IRepository.cs         - DAL contract
- Models/Repository.cs          - DAL implementation (EF Core)
- Controllers/PatientController.cs
- Views/Patient/{RegisterForMembership,CancelMembership,UpdateEmail}.cshtml

## NuGet packages
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools

## Migration (Package Manager Console)
Add-Migration InitialCreate
Update-Database

(CLI equivalent: dotnet ef migrations add InitialCreate ; dotnet ef database update)

## Notes for older templates (.NET Core 3.1 / 5.0 with Startup.cs)
Put these in Startup.ConfigureServices instead of Program.cs:
    services.AddControllersWithViews();
    services.AddDbContext<PatientInfoDbContext>();
    services.AddScoped<IRepository, Repository>();
And set the default route in Configure:
    endpoints.MapControllerRoute("default", "{controller=Patient}/{action=RegisterForMembership}/{id?}");

Start page: /Patient/RegisterForMembership

## Verification status
Built and exercised end to end on 2026-09-03 (.NET SDK 8.0.424 building the net6.0 target):
- `dotnet build` - succeeded, 0 errors (only the net6.0 end-of-support warning).
- `dotnet ef migrations add InitialCreate` against the SqlServer provider generates the
  exact table from the spec: int IDENTITY(1,1) PK, varchar(25/10/4/10/30), NOT NULL on
  every column except BloodGroup.
- App run against a SQLite copy of the DbContext and driven over HTTP:
  - Register with valid data -> row inserted, identity RegistrationID returned in the message.
  - Register with invalid data -> all six data annotations reported, nothing inserted.
  - UpdateEmail valid / bad email format / unknown RegistrationID -> correct message each time.
  - CancelMembership unknown id -> "No record found"; existing id -> deleted, second delete
    correctly reports not found.

## Known spec ambiguity
The question lists BloodGroup as `Required` in the model bullet list, but the database table
shows `Varchar(4)` with no NOT NULL, and the "MVC model data annotation" section names only
PatientName, Age, Gender, ContactNumber and EmailID as required. This code follows the latter
two (BloodGroup optional). To make it required, add `[Required]` to the BloodGroup property
in Models/PatientInfoDetail.cs.
