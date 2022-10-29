using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace Poq.ProductService.Infrastructure.Http;

public static class Policies
{
    private const string SleepDurationKey = "Broken";

    public static IAsyncPolicy<HttpResponseMessage> TransientErrorFor<T>(IServiceProvider serviceProvider, HttpRequestMessage request)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<T>>();

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
            .WaitAndRetryForeverAsync((_, ctx) =>
                {
                    logger.LogWarning("Retrying {Method} request to {RequestUri}",
                        request.Method, request.RequestUri);

                    return ctx.ContainsKey(SleepDurationKey)
                        ? (TimeSpan)ctx[SleepDurationKey]
                        : TimeSpan.FromSeconds(15);
                });
    }

    public static IAsyncPolicy<HttpResponseMessage> CircuitBreakerFor<T>(IServiceProvider serviceProvider, HttpRequestMessage request)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<T>>();

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromMinutes(3),
                (dr, ts, ctx) =>
                {
                    logger.LogWarning("{Method} request to {RequestUri} failed. Breaking circuit for {Client}",
                        request.Method, request.RequestUri, typeof(T).Name);

                    ctx[SleepDurationKey] = ts;
                }, ctx =>
                {
                    logger.LogWarning("Circuit for {Client} reset", typeof(T).Name);

                    ctx.Remove(SleepDurationKey);
                }
            );
    }

    public static Func<IServiceProvider, HttpRequestMessage, AsyncTimeoutPolicy<HttpResponseMessage>> TimeoutFor<T>()
    {
        return (serviceProvider, request) =>
        {
            var logger = serviceProvider.GetRequiredService<ILogger<T>>();

            return Policy
                .TimeoutAsync<HttpResponseMessage>(
                    TimeSpan.FromSeconds(5),
                    (_, _, _, _) =>
                    {
                        logger.LogWarning("{Method} request to {RequestUri} timed out",
                            request.Method, request.RequestUri);
                        return Task.CompletedTask;
                    });
        };
    }
}
