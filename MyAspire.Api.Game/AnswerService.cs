using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Messaging.ServiceBus; 
using MyAspire.Database;
using System.Text.Json;
namespace MyAspire.Api.Game;
public class AnswerService(ServiceBusClient serviceBusClient){
    [Function(nameof(AnswerService))]
    public async Task SaveAnswer([HttpTrigger(AuthorizationLevel.Function, "post", Route = "answers")] HttpRequest req)
    {
        // Read the request body
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var answer = JsonSerializer.Deserialize<Answer>(requestBody);

        var sender = serviceBusClient.CreateSender("answers");
        var message = new ServiceBusMessage(JsonSerializer.Serialize(answer));
        await sender.SendMessageAsync(message);

    }
}