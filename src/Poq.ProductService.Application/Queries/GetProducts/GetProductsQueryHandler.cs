using Poq.ProductService.Application.Interfaces;
using Poq.ProductService.Application.Models;
using Poq.ProductService.Application.Services;

namespace Poq.ProductService.Application.Queries.GetProducts;

public sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, Response>
{
    private readonly IProductService _productService;

    public GetProductsQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<Response> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        // Fetch data from mocky.io
        var products = await _productService.GetAll();

        if (products is null)
        {
            return Response.Empty;
        }

        return new ResponseBuilder()
                .WithProducts(products.Products)
                .WithMinPrice(query.MinPrice)
                .WithMaxPrice(query.MaxPrice)
                .WithSize(query.Size)
                .WithHighlight(query.Highlight)
                .WithSuccess()
                .Build();
    }
}
