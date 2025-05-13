using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

public class Ping(){
    [Function(nameof(Ping))]
    public string PingMe([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ping")] HttpRequest req)
    {
        return "Pong";
    }
}