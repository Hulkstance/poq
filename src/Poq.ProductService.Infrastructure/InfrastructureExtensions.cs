using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poq.ProductService.Application.Services;
using Poq.ProductService.Infrastructure.Clients;
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
            .AddHttpClient<IMockyClient, MockyClient>(client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue<string>("Endpoints:MockyUrl"));
            })
            // .AddPolicyHandler(Policies.TransientErrorFor<IMockyClient>)
            // .AddPolicyHandler(Policies.CircuitBreakerFor<IMockyClient>)
            .AddPolicyHandler(Policies.TimeoutFor<IMockyClient>())
            .AddHttpMessageHandler<LoggingHandler>();

        return services;
    }
}
