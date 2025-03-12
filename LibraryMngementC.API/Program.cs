using LibraryManagement.Application;
using LibraryManagementC.Persistance;
using LibraryManagementC.Persistance.Extensions;
using LibraryMngementC.API.Extensions;
using LibraryMngementC.Identity;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.ConfigurePersistenceService(configuration);
builder.Services.ConfigureIdentityService(configuration);

builder.Services.ConfigureApplicationService(configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalhostPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
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

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

app.MapEndPoint();

app.UseCors("LocalhostPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MigrateDatabase();
}

app.UseHttpsRedirection();

app.Run();

