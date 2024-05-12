using Serilog;
using HubtelWallet.Application;
using HubtelWallet.Infrastructure;
using System.Text.Json.Serialization;
using HubtelWallet.API.Middlewares;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
        .AddControllers(cfg =>
        {
            cfg.Filters.Add(new ValidationActionFilter());
        })
        .AddJsonOptions(cfg =>
        {
            // serialize enums as strings in api responses
            cfg.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add layers

builder.Services
    .AddApplicationLayer()
    .AddInfrastructureLayer(builder.Configuration)
    .AddTransient<ExceptionHandlingMiddleware>()
    .Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration.WriteTo.Console();
});

var app = builder.Build();

await app.Services.InitializeDatabaseAsync();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
