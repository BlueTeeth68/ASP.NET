using CQRS.Api.ConfigurationOptions;
using CQRS.Infrastructure.Web.GlobalExceptions;
using CQRS.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
var configuration = builder.Configuration;

var appSetting = new AppSettings();
configuration.Bind(appSetting);

services.Configure<AppSettings>(configuration);

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddSerilog();

//problem detail
services.AddProblemDetails();

// services.AddExceptionHandler<GlobalExceptionsHandler>(null);

//Add persistence services
services.AddPersistence(appSetting.ConnectionStrings.DefaultConnection);

var app = builder.Build();

// app.UseExceptionHandler(options => { });

app.UseMiddleware<GlobalExceptionsHandler>();
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