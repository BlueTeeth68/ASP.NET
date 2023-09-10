using Application.Configurations;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Hellang.Middleware.ProblemDetails;
using Microsoft.OpenApi.Models;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Add problem detail
builder.Services.AddProblemDetails();

//Add dependency injection
var configuration = builder.Configuration.Get<AppConfiguration>();
configuration.DatabaseConnection = builder.Configuration.GetConnectionString("DefaultConnection");
configuration.Key = builder.Configuration["JwtSettings:Key"];
builder.Services.AddDependency(configuration.DatabaseConnection);
builder.Services.AddSingleton(configuration);

//allow enum string value in swagger and front-end instead of int value
builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

//Add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //ValidateIssuer = true,
            //ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            //ValidIssuer = _configuration["Jwt:Issuer"],
            //ValidAudience = _configuration["Jwt:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Key)),
            ClockSkew = TimeSpan.Zero
        };
    }
);

//Auhtorization
builder.Services.AddPolicy();
// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("Admin", policy => { policy.RequireRole(Role.Admin.ToString()); });
//     options.AddPolicy("Manager", policy => { policy.RequireRole(Role.Manager.ToString()); });
//     options.AddPolicy("ManagerOrAdmin",
//         policy => { policy.RequireRole(Role.Manager.ToString(), Role.Admin.ToString()); });
// });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();