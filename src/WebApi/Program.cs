using Data;
using Microsoft.OpenApi.Models;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddServices();
builder.Services.AddData();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = ".NET Core Clean Architecture Starter API",
        Version = "v1",
        Description = "A small .NET Core Web API that separates Domain, Services, Data, and WebApi concerns."
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", ".NET Core Clean Architecture Starter API v1");
});

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
