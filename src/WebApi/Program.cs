using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Force URL so you always know where it runs
builder.WebHost.UseUrls("http://localhost:5000");

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DotNetCore Starter Template API",
        Version = "v1",
        Description = "A minimal .NET 8 Web API starter with health + todos endpoints."
    });
});

var app = builder.Build();

// No HTTPS redirect to avoid port issues
// app.UseHttpsRedirection();

// Swagger ALWAYS enabled
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotNetCore Starter Template API v1");
});

app.UseRouting();

app.UseAuthorization();

// Map Controllers
app.MapControllers();

app.Run();
