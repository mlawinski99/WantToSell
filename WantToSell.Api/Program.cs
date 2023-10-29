using Microsoft.Extensions.DependencyInjection.Extensions;
using WantToSell.Api.Middleware;
using WantToSell.Application;
using WantToSell.Identity;
using WantToSell.Infrastructure;
using WantToSell.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServicesCollection();
builder.Services.AddInfrastructureServicesCollection();
builder.Services.AddPersistenceServicesCollection(builder.Configuration);
builder.Services.AddIdentityServicesRegistration(builder.Configuration);

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll",
		builder =>
			builder.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod());
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

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
