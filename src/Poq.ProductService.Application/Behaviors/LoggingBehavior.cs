using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Poq.ProductService.Application.Behaviors;

public sealed class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        _logger.LogInformation("Request: {Name} {@Request}", requestName, request);
        return Task.CompletedTask;
    }
}
