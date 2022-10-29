using Poq.ProductService.Application.Models;

namespace Poq.ProductService.Application.Extensions;

public static class ProductExtensions
{
    private const int MostCommonWordsToSkip = 5;
    private const int MostCommonWordsToTake = 10;

    public static IEnumerable<Product> FilteredSubsetOfProducts(
        this IEnumerable<Product> products,
        double? minPrice = default,
        double? maxPrice = default,
        string[]? size = default)
    {
        ArgumentNullException.ThrowIfNull(products);

        var filteredProducts = products;

        if (minPrice.HasValue)
        {
            filteredProducts = filteredProducts.Where(x => x.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            filteredProducts = filteredProducts.Where(x => x.Price <= maxPrice.Value);
        }

        if (size is not null)
        {
            filteredProducts = filteredProducts
                .Where(x => x.Sizes
                    .Intersect(size, StringComparer.InvariantCultureIgnoreCase)
                    .Any())
                .ToList();
        }

        return filteredProducts;
    }

    public static IEnumerable<Product> Highlight(this IEnumerable<Product> products, string[]? keywords = default)
    {
        ArgumentNullException.ThrowIfNull(products);

        if (keywords is null)
        {
            return products;
        }

        var result = products.Select(x => x with
        {
            Description = keywords
                .Aggregate(x.Description, (phrase, word) =>
                {
                    var pattern = $"<em>{word}</em>";
                    return phrase.Contains(pattern)
                        ? phrase
                        : phrase.Replace(word, pattern);
                })
        });

        return result;
    }

    public static IEnumerable<string> GetCommonWords(this IEnumerable<Product> products)
    {
        return products
            .SelectMany(x => x.Description
                .Remove(x.Description.Length - 1)
                .Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .GroupBy(word => word)
            .OrderByDescending(x => x.Count())
            .Skip(MostCommonWordsToSkip)
            .Take(MostCommonWordsToTake)
            .Select(x => x.Key)
            .ToList();
    }
}
