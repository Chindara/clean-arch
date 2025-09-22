# Clear Architecture Solution Template 

This template provides a seamless solution by integrating the benefits of Clean Architecture with ASP.NET Core. It allows you to swiftly create an ASP.NET Core Web API, adhering to Clean Architecture principles. The template is pre-configured to use SQL Server, and upon running the application, the database will be automatically created, with the latest migrations applied.

If you find this project useful, please give it a star. Thanks! ⭐

## Technologies

![.NET 8](https://i.imgur.com/quop3E8.png)
![EF Core 8](https://i.imgur.com/AcYaj2y.png)
![Serilog](https://i.imgur.com/i8xjWjs.png)
![Mediatr](https://i.imgur.com/orjrsa3.png)
![Fluent Validation](https://i.imgur.com/acPuZJW.png)
![MailTrap](https://i.imgur.com/OpILNdy.png)

## Getting Started

The following prerequisites are required to build and run the solution:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (Latest version)
- [MailTrap](https://mailtrap.io/) (If you are using the email feature)

Run the following command to install the .NET template from Nuget
```dotnetcli
dotnet new install Clean.Architecture.Template.CSharp::1.0.2
```

`dotnet new list` - Lists available templates to be run using `dotnet new`.

`dotnet new uninstall` - Uninstalls the template

## Create a new solution
Use the following command to create the Clean Architecture WebAPI solution

```dotnetcli
dotnet new ca-sln -o "<Name of the project>"
```

### Configurations
Once the solution is created, go to `appsettings.json` file & update the followings

#### Database (MSSQL Server)
`"ConnectionStrings"` section with your connection string.

#### Email (MailTrap)
 `"EmailSettings"` section with your [MailTrap](https://mailtrap.io/) details.

 ## Build Nuget Package
 - Update the Version in `CleanArchitectureTemplate.csproj` file
 - Run the below command
```dotnetcli
dotnet pack CleanArchitectureTemplate.csproj -c Release -o ./nupkgs
```