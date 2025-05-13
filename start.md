# Start

## Resources
- [Aspire Samples](https://github.com/dotnet/aspire-samples.git)

## Initial Steps
1. Download and install FluentUI Templates
    ```cmd
    dotnet new install Microsoft.FluentUI.AspNetCore.Templates
    ```
2. Install Vs Code
3. Install C# Dev Kit
4. Debug Application
    - Choose C#
    - Choose `<ProjectName>`
    - Choose `<ProjectName>.AppHost.csproj`

## Database Steps
1. Create new worker project `<ProjectName>.Database.csproj`
2. Run:
    ```cmd
    dotnet new classlib
    ```
3. Install Tools:
    ```cmd
    dotnet tool install --global dotnet-ef
    ```
4. Install libraries:
    ```cmd
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.EntityFrameworkCore.Tools
    dotnet add reference ..\<Project>.ServiceDefaults\<Project>.ServiceDefaults.csproj
    ```
5. Add `ApiDbInitializer` file.
6. Register Database in AppHost:
    ```csharp
    <PackageReference Include="Aspire.Hosting.SqlServer" Version="9.2.0" />
    var sqlserver = builder.AddSqlServer("sqlserver").WithLifetime(ContainerLifetime.Persistent);
    var db1 = sqlserver.AddDatabase("db1");
    ```
7. Add Database Project Reference to AppHost:
    ```xml
    <ProjectReference Include="..\MyAspire.Database\MyAspire.Database.csproj" />
    ```
8. Add migration:
    ```csharp
    var migrationService = builder.AddProject<Projects.MyAspire_Database>("migration")
        .WithReference(db1)
        .WaitFor(db1);
    ```
9. Add access to API service:
    ```csharp
    var apiService = builder.AddProject<Projects.MyAspire_ApiService>("apiservice")
        .WithExternalHttpEndpoints()
        .WithReference(db1)
        .WaitFor(db1);
    ```
10. Add Entities, Seed Data, and Migrations.

## Redis Cache Steps
[Redis Integration Documentation](https://learn.microsoft.com/en-us/dotnet/aspire/caching/stackexchange-redis-integration?tabs=dotnet-cli&pivots=redis)

1. On App Host Proejct:
    1. ```cmd
        dotnet add package Aspire.Hosting.Redis
        ```
    2. Add Redis to the builder:
        ```csharp
        var cache = builder.AddRedis("cache")
            .WithLifetime(ContainerLifetime.Persistent)
            .WithDataVolume(isReadOnly: false)
            .WithRedisInsight();
        ```
    3. Add Reference:
        ```csharp
        builder.AddProject<MyAspire.Api>().WithReference(cache);
        ```
2. Run on Client Project:
    1.  ```cmd
        dotnet add package Aspire.StackExchange.Redis
        ```
    2. Add Redis Client to `Program.cs`:
        ```csharp
        builder.AddRedisClient(connectionName: "cache");
        ```

## Service Bus
[dotnet add package Microsoft.Extensions.Caching.Hybrid](dotnet add package Microsoft.Extensions.Caching.Hybrid)

1. On App Host Project:
    1. Install the package:
        ```cmd
        dotnet add package Aspire.Hosting.Azure.ServiceBus
        ```
    2. Add Azure Service Bus to the builder:
        ```csharp
        var serviceBus = builder.AddAzureServiceBus("messaging")
            .RunAsEmulator(emulator => { emulator.WithHostPort(7777); });
        ```
    3. Add a Service Bus Queue:
        ```csharp
        var queue = serviceBus.AddServiceBusQueue("queue");
        var answerQueu = serviceBus.AddServiceBusQueue("answer-queue");
        ```
2. On Client Project:
    1. Add Azure Service Bus Client:
        ```csharp
        builder.AddAzureServiceBusClient(connectionName: "messaging");
        ```
    2. Inject the Service Bus Client:
        ```csharp
        @inject ServiceBusClient sbClient
        ```
    3. Send a message:
        ```csharp
        var sender = sbClient.CreateSender("queue");
        var message = new ServiceBusMessage(messageBody);
        return sender.SendMessageAsync(message);
        ```

## React App 
1. Install NodeJs
    ```cmd
    winget install OpenJS.NodeJS
    ```
2. Install NextJS
    ```cmd
    npx create-next-app@latest
    ```
3. Modify Page.js
4. Connect Set up Api
    ```javascript
    export const dynamic = 'force-static'

    export async function GET(req) {
        const { url } = req;
        const apiservice = process.env.services__apiservice__https__0 ||
            process.env.services__apiservice__http__0
        // console.log(JSON.stringify(req))
        const fetchUrl = `${apiservice}/${url.replace('http://localhost:3000/api/', '')}`
        console.log(fetchUrl)
        const res = await fetch(`${fetchUrl}`)
        const data = await res.json()

        return Response.json({ data })
    }
    ```
5. Create .env file to set local variable for development 
    ```cmd
    NODE_TLS_REJECT_UNAUTHORIZED = '0'
    ```
6. App Host Project Setup 
    1. Add Node Package 
        ```xml
        <PackageReference Include="Aspire.Hosting.NodeJs" Version="*" />
        ```
    2. Register NodeJs App
        ```javascript
        builder.AddNpmApp("web-game", "../MyAspire.Web.Game/web-game", "dev")
        .WithReference(apiService)
        .WaitFor(apiService)
        .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
        .WithHttpEndpoint(env: "PORT")
        .WithExternalHttpEndpoints();
        ```
## Functions Api 
1. Install Function CLI 
    ```cmd
    winget install Microsoft.Azure.FunctionsCoreTools
    ```
2. Create a new Api Project, after creating a folder run the following
    ```cmd
    func init
    ```
    choose dotnet-isolated, c#-isolated
3. 