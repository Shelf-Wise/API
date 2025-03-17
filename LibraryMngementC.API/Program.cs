using LibraryManagement.Application;
using LibraryManagementC.Persistance;
using LibraryManagementC.Persistance.Extensions;
using LibraryMngementC.API.Extensions;
using LibraryMngementC.API.Middleware;
using LibraryMngementC.Identity;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.ConfigurePersistenceService(configuration);
builder.Services.ConfigureIdentityService(configuration);

builder.Services.ConfigureApplicationService(configuration);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddAzureWebAppDiagnostics();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a Valid Token NO NEED THE KEYWORD BEARER JUST PSATE THE TOKEN",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer",
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
            new string[]{ }
        }
    });
});

// Add this before var app = builder.Build();
if (!builder.Environment.IsDevelopment())
{
    builder.Services.Configure<LoggerFilterOptions>(options =>
    {
        options.MinLevel = LogLevel.Information;
    });
}

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

app.MapEndPoint();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MigrateDatabase();
}

// Add this after app.UseHttpsRedirection();
app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseHttpsRedirection();

app.Run();

