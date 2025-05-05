using Aspire.Hosting.Azure;

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

builder.AddProject<Projects.MyAspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(db1)
    .WithReference(apiService)
    .WithReference(serviceBus)
    .WithReference(queue)
    .WaitFor(apiService);



builder.Build().Run();
