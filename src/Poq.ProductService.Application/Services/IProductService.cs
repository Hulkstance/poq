using Poq.ProductService.Application.Models;

namespace Poq.ProductService.Application.Services;

public interface IProductService
{
    Task<GetProductsResponse?> GetProductsAsync();
}
