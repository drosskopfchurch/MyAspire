# Start

## Resources
- [Aspire Samples](https://github.com/dotnet/aspire-samples.git)


## Initial Steps 
1. Download and install FluentUI Templates
```dotnet new install Microsoft.FluentUI.AspNetCore.Templates```
2. Install Vs Code
3. Install C# Dev Kit
4. Debug Application
    - Choose C# 
    - Choose <ProjectName> 
    - Choose <ProjectName>.AppHost.csproj


## Database Steps 
1. Create new worker project <ProjectName>.Database.csproj
2. run ```dotnet new classlib```
3. Install Tools
```dotnet tool install --global dotnet-ef```
4. install libraries
- ```dotnet add package Microsoft.EntityFrameworkCore.SqlServer```
- ```dotnet add package Microsoft.EntityFrameworkCore.Tools```
- ```dotnet add reference ..\<Project>.ServiceDefaults\<Project>.ServiceDefaults.csproj```
5. Add ApiDbInitilizerFile
6. Register Database in AppHost
- ```<PackageReference Include="Aspire.Hosting.SqlServer" Version="9.2.0" />```
- ```var sqlserver = builder.AddSqlServer("sqlserver").WithLifetime(ContainerLifetime.Persistent);```
- ```var db1 = sqlserver.AddDatabase("db1");```
7. Add Database Project Reference to AppHost
- ```<ProjectReference Include="..\MyAspire.Database\MyAspire.Database.csproj" /> ```
9. Add migration 
- ```var migrationService = builder.AddProject<Projects.MyAspire_Database>("migration").WithReference(db1).WaitFor(db1);```
9. Add access to api service
- ```var apiService = builder.AddProject<Projects.MyAspire_ApiService>("apiservice").WithExternalHttpEndpoints().WithReference(db1).WaitFor(db1);  ```
10. Add Entities, Seed Data, Migrations


## Redis Cache Steps
[https://learn.microsoft.com/en-us/dotnet/aspire/caching/stackexchange-redis-integration?tabs=dotnet-cli&pivots=redis](https://learn.microsoft.com/en-us/dotnet/aspire/caching/stackexchange-redis-integration?tabs=dotnet-cli&pivots=redis)

1. ```dotnet add package Aspire.Hosting.Redis``` run on app host
2. ```var cache = builder.AddRedis("cache").WithLifetime(ContainerLifetime.Persistent).WithDataVolume(isReadOnly: false).WithRedisInsight();``` Add Reference 
3. ```builder.AddProject<MyAspire.Api>().WithReference(cache);``` Add Reference
4. ```dotnet add package Aspire.StackExchange.Redis``` Run on client project
5. ```builder.AddRedisClient(connectionName: "cache");``` add to client program.cs

