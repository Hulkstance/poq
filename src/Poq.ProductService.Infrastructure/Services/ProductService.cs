using System.Net;
using System.Net.Http.Json;
using Poq.ProductService.Application.Models;
using Poq.ProductService.Application.Services;

namespace Poq.ProductService.Infrastructure.Services;

public sealed class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<GetProductsResponse?> GetProductsAsync()
    {
        var httpClient = _httpClientFactory.CreateClient("Mocky");

        var productResponse = await httpClient.GetAsync("/v2/5e307edf3200005d00858b49");
        if (productResponse.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
        var product = await productResponse.Content.ReadFromJsonAsync<GetProductsResponse>();
        return product;
    }
}
