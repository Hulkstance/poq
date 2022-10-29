using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Poq.ProductService.Api.Configurations;

internal static class ConfigureCorsExtensions
{
    private const string CorsPolicyName = "CorsPolicy";

    public static IServiceCollection ConfigureCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(CorsOptions);
        return services;
    }

    public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
    {
        app.UseCors(CorsPolicyName);
        return app;
    }

    private static void CorsOptions(CorsOptions options)
    {
        options.AddPolicy(CorsPolicyName,
            builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .Build()
        );
    }
}
