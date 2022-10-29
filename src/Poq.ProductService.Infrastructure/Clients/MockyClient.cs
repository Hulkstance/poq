using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Poq.ProductService.Application.Models;

namespace Poq.ProductService.Infrastructure.Clients;

public interface IMockyClient
{
    Task<GetProductsResponse?> GetProductsAsync();
}

public sealed class MockyClient : IMockyClient
{
    private readonly ILogger<MockyClient> _logger;
    private readonly HttpClient _httpClient;

    public MockyClient(ILogger<MockyClient> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<GetProductsResponse?> GetProductsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<GetProductsResponse>("/v2/5e307edf3200005d00858b49");
        return response;
    }
}
