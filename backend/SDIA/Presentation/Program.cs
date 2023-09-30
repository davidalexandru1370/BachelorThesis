using SDIA.Configurations;
using SDIA.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var host = builder.Host;
services.ConfigureServices(builder.Configuration);
host.ConfigureHost(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
