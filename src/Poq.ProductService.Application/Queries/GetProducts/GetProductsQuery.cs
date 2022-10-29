using Poq.ProductService.Application.Interfaces;
using Poq.ProductService.Application.Models;

namespace Poq.ProductService.Application.Queries.GetProducts;

public record GetProductsQuery(
    double? MinPrice = default,
    double? MaxPrice = default,
    string[]? Size = default,
    string[]? Highlight = default) : IQuery<Response>;
