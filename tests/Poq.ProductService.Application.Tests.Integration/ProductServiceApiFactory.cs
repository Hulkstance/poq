using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Poq.ProductService.Api;
using Xunit;

namespace Poq.ProductService.Tests.Integration;

public sealed class ProductServiceApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    public MockyApiServer MockyApiServer { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });

        builder.ConfigureTestServices(services =>
        {
            services.AddHttpClient("Mocky", client =>
            {
                client.BaseAddress = new Uri(MockyApiServer.Url);
            });
        });
    }

    public Task InitializeAsync()
    {
        MockyApiServer.Start();
        return Task.CompletedTask;
    }

    public new Task DisposeAsync()
    {
        MockyApiServer.Dispose();
        return Task.CompletedTask;
    }
}
