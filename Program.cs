using ApiAggregator.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddHttpClient<OpenMeteoClient>();

builder.Services.AddHttpClient<OpenWeatherClient>();

//builder.Services.AddHttpClient<IOpenWeatherClient, OpenWeatherClient>(client =>
//{
//    client.BaseAddress = new Uri(builder.Configuration["ExternalApis:ApiA"] ?? "https://api-a.example.com/");
//    client.DefaultRequestHeaders.Add("Accept", "application/json");
//}).AddPolicyHandler(HttpPolicies.GetDefaultPolicy());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
