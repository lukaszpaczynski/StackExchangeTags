using Application;
using Domain.Abstractions;
using Infrastructure;
using Infrastructure.Database;
using Infrastructure.HttpServices;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddCarter();
builder.Services.AddScoped<IStackExchangeApiClient, StackExchangeApiClient>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "StackExchangeTags", Version = "v1" });
});

var app = builder.Build();

app.MapCarter();
await app.InitialiseDatabaseAsync();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			c.RoutePrefix = string.Empty;
		});
}

app.Run();
