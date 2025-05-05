var builder = DistributedApplication.CreateBuilder(args);

var sqlserver = builder.AddSqlServer("sqlserver")
    .WithLifetime(ContainerLifetime.Persistent);
var db1 = sqlserver.AddDatabase("db1");

var cache = builder.AddRedis("cache")
    .WithLifetime(ContainerLifetime.Persistent)
            .WithDataVolume(isReadOnly: false)
            .WithRedisInsight();

var migrationService = builder.AddProject<Projects.MyAspire_Database>("migration")
    .WithReference(db1)
    .WaitFor(db1);

var apiService = builder.AddProject<Projects.MyAspire_ApiService>("apiservice")
    .WithExternalHttpEndpoints()
    .WithReference(db1)
    .WithReference(cache)
    .WaitFor(migrationService);  

builder.AddProject<Projects.MyAspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(db1)
    .WithReference(apiService)
    .WaitFor(apiService);



builder.Build().Run();
