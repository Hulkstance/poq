using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poq.ProductService.Application.Services;
using Poq.ProductService.Infrastructure.Http;

namespace Poq.ProductService.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddSingleton<IProductService, Services.ProductService>();

        // Fault-tolerant HTTP client that logs the mocky.io response
        services.AddTransient<LoggingHandler>();

        services
            .AddHttpClient("Mocky", client =>
            {
                client.BaseAddress = new Uri("http://www.mocky.io");
            })
            // .AddPolicyHandler(Policies.TransientErrorFor<IProductService>)
            // .AddPolicyHandler(Policies.CircuitBreakerFor<IProductService>)
            .AddPolicyHandler(Policies.TimeoutFor<IProductService>())
            .AddHttpMessageHandler<LoggingHandler>();

        return services;
    }
}
