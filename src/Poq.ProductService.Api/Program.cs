using Poq.ProductService.Api.Caching;
using Poq.ProductService.Api.Configurations;
using Poq.ProductService.Api.Endpoints;
using Poq.ProductService.Api.Middlewares;
using Poq.ProductService.Application;
using Poq.ProductService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging();
builder.Services.ConfigureJsonOptions();
builder.Services.ConfigureCorsPolicy();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddSwagger();

_ = builder.Configuration.GetValue<string>("OutputCacheProvider") == "Redis"
    ? builder.Services.AddRedisOutputCache()
    : builder.Services.AddOutputCache();

builder.Services.AddProductEndpoints();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseAppSwagger();
}

app.UseHttpsRedirection();
app.UseCorsPolicy();;
app.UseOutputCache();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseProductEndpoints();

app.Run();
