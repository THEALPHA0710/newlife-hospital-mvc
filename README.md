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
