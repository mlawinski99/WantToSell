using Microsoft.OpenApi.Models;
using Serilog;
using WantToSell.Api.Middleware;
using WantToSell.Application;
using WantToSell.Cache;
using WantToSell.Identity;
using WantToSell.Infrastructure;
using WantToSell.Persistence;
using WantToSell.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
    .WriteTo.Console()
    .ReadFrom.Configuration(context.Configuration));


builder.Services.AddApplicationServicesCollection();
builder.Services.AddInfrastructureServicesCollection();
builder.Services.AddPersistenceServicesCollection(builder.Configuration);
builder.Services.AddIdentityServicesRegistration(builder.Configuration);
builder.Services.AddStorageServicesCollection();
builder.Services.AddCacheHelpersCollection(builder.Configuration);

builder.Services.AddSwaggerGen(opts => {
    opts.MapType(typeof(IFormFile), () => new OpenApiSchema() { Type = "file", Format = "binary" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
});

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    option.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        }
    );
    option.AddSecurityRequirement(
        new OpenApiSecurityRequirement
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
        }
    );
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<UserIdMiddleware>();

app.MapControllers();

app.Run();