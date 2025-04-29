var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.MyAspire_ApiService>("apiservice");

builder.AddProject<Projects.MyAspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
