using Aspire.Hosting.Azure;
using Azure.Provisioning.Storage;

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



var serviceBus = builder.AddAzureServiceBus("messaging").RunAsEmulator(emulator =>
{
    emulator.WithHostPort(7777);
});
var queue = serviceBus.AddServiceBusQueue("queue");
var answers = serviceBus.AddServiceBusQueue("answers");
var topic = serviceBus.AddServiceBusTopic("topic");
topic.AddServiceBusSubscription("sub1")
     .WithProperties(subscription =>
     {
         subscription.MaxDeliveryCount = 10;
         subscription.Rules.Add(
             new AzureServiceBusRule("app-prop-filter-1")
             {
                 CorrelationFilter = new()
                 {
                     ContentType = "application/text",
                     CorrelationId = "id1",
                     Subject = "subject1",
                     MessageId = "msgid1",
                     ReplyTo = "someQueue",
                     ReplyToSessionId = "sessionId",
                     SessionId = "session1",
                     SendTo = "xyz"
                 }
             });
     });
var storage = builder.AddAzureStorage("storage").RunAsEmulator()
    .ConfigureInfrastructure((infrastructure) =>
    {
        var storageAccount = infrastructure.GetProvisionableResources().OfType<StorageAccount>().FirstOrDefault(r => r.BicepIdentifier == "storage")
            ?? throw new InvalidOperationException($"Could not find configured storage account with name 'storage'");

        // Ensure that public access to blobs is disabled
        storageAccount.AllowBlobPublicAccess = false;
    });

var apiGame = builder.AddAzureFunctionsProject<Projects.MyAspire_Api_Game>("api-game")
    .WithExternalHttpEndpoints()
    .WithReference(serviceBus)
    .WithReference(answers)
    .WaitFor(storage)
    .WaitFor(serviceBus)
    .WithRoleAssignments(storage,
        // Storage Account Contributor and Storage Blob Data Owner roles are required by the Azure Functions host
        StorageBuiltInRole.StorageAccountContributor, StorageBuiltInRole.StorageBlobDataOwner,
        // Queue Data Contributor role is required to send messages to the queue
        StorageBuiltInRole.StorageQueueDataContributor)
    .WithHostStorage(storage);

builder.AddProject<Projects.MyAspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(db1)
    .WithReference(apiService)
    .WithReference(serviceBus)
    .WithReference(queue)
    .WaitFor(apiService);

builder.AddNpmApp("web-game", "../MyAspire.Web.Game/web-game", "dev")
    .WithReference(apiService)
    .WithReference(apiGame)
    .WaitFor(apiService)
    .WaitFor(apiGame)
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints();




builder.Build().Run();
