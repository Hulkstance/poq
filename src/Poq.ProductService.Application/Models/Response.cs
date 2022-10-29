namespace Poq.ProductService.Application.Models;

public sealed class Response
{
    /// <summary>
    /// Whether it was successful or not.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The error message if any.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// The products list.
    /// </summary>
    public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();

    /// <summary>
    /// Gets the minimum price in the product list.
    /// </summary>
    public double MinPrice => Products.Any() ? Products.Min(p => p.Price) : 0;

    /// <summary>
    /// Gets the maximum price in the product list.
    /// </summary>
    public double MaxPrice => Products.Any() ? Products.Max(p => p.Price) : 0;

    /// <summary>
    /// Gets all product sizes in the product list.
    /// </summary>
    public IEnumerable<string> AllSizes => Products
        .SelectMany(p => p.Sizes)
        .Distinct();

    /// <summary>
    /// A list of common words in the product list descriptions, excluding top 5 and taking maximum 10 words.
    /// </summary>
    public IEnumerable<string> CommonWords { get; set; } = Enumerable.Empty<string>();

    public static Response Empty => new();
}
