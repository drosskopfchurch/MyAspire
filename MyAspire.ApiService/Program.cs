using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddDbContext<MyAspireDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("db1")));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHybridCache();

builder.AddRedisClient(connectionName: "cache");

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.MapGet("/questions", async (MyAspireDbContext context, HybridCache hybridCache) => {
    return await hybridCache.GetOrCreateAsync<List<Question>>(
        key:"questions", 
        factory: async (cancellationToken) => await context.Questions.ToListAsync(cancellationToken),
        cancellationToken: default
    );
})
.WithName("GetQuestions");

app.MapDefaultEndpoints();

app.Run();
