# Clear Architecture Solution Template 

This template provides a seamless solution by integrating the benefits of Clean Architecture with ASP.NET Core. It allows you to swiftly create an ASP.NET Core Web API, adhering to Clean Architecture principles. The template is pre-configured to use SQL Server, and upon running the application, the database will be automatically created, with the latest migrations applied.

If you find this project useful, please give it a star. Thanks! ⭐

## Getting Started

The following prerequisites are required to build and run the solution:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (latest version)

Clone the repository to your computer. Once you are inside the root directory run the following command in PowerShell

```
dotnet new install .
```

Once installed, create a new solution using the template. 

```
dotnet new ca-sln -o "<Name of the project>"
```

To create a new feature
```
dotnet new ca-feature -p <Name of the project> -f <Name of the feature>
```

## Technologies

![.NET 8](https://i.imgur.com/quop3E8.png)
![EF Core 8](https://i.imgur.com/AcYaj2y.png)
![Serilog](https://i.imgur.com/i8xjWjs.png)
![Mediatr](https://i.imgur.com/orjrsa3.png)
![Fluent Validation](https://i.imgur.com/acPuZJW.png)

### Template Commands
```
dotnet new list
```
```
dotnet new install . --force
```
```
dotnet new uninstall
```
