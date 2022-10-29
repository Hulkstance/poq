using Poq.ProductService.Application.Extensions;

namespace Poq.ProductService.Application.Models;

/// <summary>
///     Regular Builder pattern.
/// </summary>
public sealed class ResponseBuilder
{
    private IEnumerable<Product>? _products;
    private double? _minPrice;
    private double? _maxPrice;
    private string[]? _size;
    private string[]? _highlight;
    private bool _success;
    private string _message = string.Empty;

    public ResponseBuilder WithProducts(IEnumerable<Product> products)
    {
        ArgumentNullException.ThrowIfNull(products);

        _products = products;
        return this;
    }

    public ResponseBuilder WithMinPrice(double? minPrice)
    {
        if (minPrice is < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(minPrice), "Please provide a valid min price");
        }

        _minPrice = minPrice;
        return this;
    }

    public ResponseBuilder WithMaxPrice(double? maxPrice)
    {
        if (maxPrice is < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxPrice), "Please provide a valid max price");
        }

        _maxPrice = maxPrice;
        return this;
    }

    public ResponseBuilder WithSize(string[]? size)
    {
        _size = size;
        return this;
    }

    public ResponseBuilder WithHighlight(string[]? highlight)
    {
        _highlight = highlight;
        return this;
    }

    public ResponseBuilder WithSuccess()
    {
        _success = true;
        return this;
    }

    public ResponseBuilder WithMessage(string message)
    {
        _message = message;
        return this;
    }

    public Response Build()
    {
        _products ??= new List<Product>();

        // Filter by min price, max price and size
        var filteredSubsetOfProducts = _products
            .FilteredSubsetOfProducts(_minPrice, _maxPrice, _size)
            .ToList();

        // Highlight description based on keywords
        var highlightedDescriptions = filteredSubsetOfProducts
            .Highlight(_highlight)
            .ToList();

        return new Response
        {
            Success = _success,
            Message = _message,
            Products = highlightedDescriptions,
            CommonWords = filteredSubsetOfProducts.GetCommonWords()
        };
    }
}
