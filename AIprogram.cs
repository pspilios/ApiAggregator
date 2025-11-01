//using Aggregator.Api.Infrastructure;
//using Aggregator.Api.Services;
//using Microsoft.OpenApi.Models;

//var builder = WebApplication.CreateBuilder(args);

//// Configuration
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c => {
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aggregator API", Version = "v1" });
//});

//// In-memory cache
//builder.Services.AddMemoryCache();

//// Typed HttpClients with Polly policies (policies applied in HttpPolicies)
//builder.Services.AddHttpClient<IExternalAClient, ExternalAClient>(client =>
//{
//    client.BaseAddress = new Uri(builder.Configuration["ExternalApis:ApiA"] ?? "https://api-a.example.com/");
//    client.DefaultRequestHeaders.Add("Accept", "application/json");
//}).AddPolicyHandler(HttpPolicies.GetDefaultPolicy());

//builder.Services.AddHttpClient<IExternalBClient, ExternalBClient>(client =>
//{
//    client.BaseAddress = new Uri(builder.Configuration["ExternalApis:ApiB"] ?? "https://api-b.example.com/");
//    client.DefaultRequestHeaders.Add("Accept", "application/json");
//}).AddPolicyHandler(HttpPolicies.GetDefaultPolicy());

//// Aggregator service
//builder.Services.AddScoped<IAggregatorService, AggregatorService>();

//var app = builder.Build();

//app.UseSwagger();
//app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aggregator API v1"));

//app.UseHttpsRedirection();
//app.MapControllers();

//app.Run();
