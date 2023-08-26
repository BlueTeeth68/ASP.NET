using Application.Configurations;
using Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add dependency injection
var configuration = builder.Configuration.Get<AppConfiguration>();
configuration.DatabaseConnection = builder.Configuration.GetConnectionString("DefaultConnection");
configuration.JWTAcessSecretKey = builder.Configuration["JWTAccessSecretKey"];
configuration.JWTRefreshSecretKey = builder.Configuration["JWTRefreshSecretKey"];
builder.Services.AddDependency(configuration.DatabaseConnection);
builder.Services.AddSingleton(configuration);

//allow enum string value in swagger and front-end instead of int value
builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
