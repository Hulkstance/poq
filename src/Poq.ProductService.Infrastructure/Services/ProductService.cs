using Poq.ProductService.Application.Models;
using Poq.ProductService.Application.Services;
using Poq.ProductService.Infrastructure.Clients;

namespace Poq.ProductService.Infrastructure.Services;

public sealed class ProductService : IProductService
{
    private readonly IMockyClient _mockyClient;

    public ProductService(IMockyClient mockyClient)
    {
        _mockyClient = mockyClient;
    }

    public Task<GetProductsResponse?> GetAll()
    {
        return _mockyClient.GetProductsAsync();
    }
}
