using NSubstitute;
using Poq.ProductService.Application.Models;
using Poq.ProductService.Application.Queries.GetProducts;
using Poq.ProductService.Application.Services;

namespace Poq.ProductService.Application.Tests.Unit.Fixtures;

// Share single test context across all tests.
public sealed class SharedFixture
{
    private readonly IProductService _productService = Substitute.For<IProductService>();

    public SharedFixture()
    {
        Sut = new GetProductsQueryHandler(_productService);

        var productResponse = new GetProductsResponse
        (
            new List<Product>
            {
                new(
                    "A Red Trouser",
                    10,
                    new List<string> { "small", "medium", "large" },
                    "This trouser perfectly pairs with a green shirt."
                ),
                new(
                    "A Green Trouser",
                    11,
                    new List<string> { "small" },
                    "This trouser perfectly pairs with a blue shirt."
                ),
                new(
                    "A Blue Trouser",
                    12,
                    new List<string> { "medium" },
                    "This trouser perfectly pairs with a red shirt."
                ),
                new(
                    "A Red Trouser",
                    13,
                    new List<string> { "large" },
                    "This trouser perfectly pairs with a green shirt."
                ),
                new(
                    "A Green Trouser",
                    14,
                    new List<string> { "small", "medium" },
                    "This trouser perfectly pairs with a blue shirt."
                ),
                new(
                    "A Blue Trouser",
                    15,
                    new List<string> { "small", "large" },
                    "This trouser perfectly pairs with a red shirt."
                ),
                new(
                    "A Red Trouser",
                    16,
                    new List<string> { "medium", "large" },
                    "This trouser perfectly pairs with a green shirt."
                ),
                new(
                    "A Green Trouser",
                    17,
                    new List<string>(),
                    "This trouser perfectly pairs with a blue shirt."
                ),
                new(
                    "A Blue Trouser",
                    18,
                    new List<string> { "small", "medium", "large" },
                    "This trouser perfectly pairs with a red shirt."
                ),
                new(
                    "A Red Trouser",
                    19,
                    new List<string> { "small" },
                    "This trouser perfectly pairs with a green belt."
                ),
                new(
                    "A Green Trouser",
                    20,
                    new List<string> { "medium" },
                    "This trouser perfectly pairs with a blue belt."
                ),
                new(
                    "A Blue Trouser",
                    21,
                    new List<string> { "large" },
                    "This trouser perfectly pairs with a red belt."
                ),
                new(
                    "A Red Trouser",
                    22,
                    new List<string> { "small", "medium" },
                    "This trouser perfectly pairs with a green belt."
                ),
                new(
                    "A Green Trouser",
                    23,
                    new List<string> { "small", "large" },
                    "This trouser perfectly pairs with a blue belt."
                ),
                new(
                    "A Blue Trouser",
                    24,
                    new List<string> { "medium", "large" },
                    "This trouser perfectly pairs with a red belt."
                ),
                new(
                    "A Red Trouser",
                    25,
                    new List<string>(),
                    "This trouser perfectly pairs with a green belt."
                ),
                new(
                    "A Green Shirt",
                    10,
                    new List<string> { "small", "medium", "large" },
                    "This shirt perfectly pairs with a blue hat."
                ),
                new(
                    "A Blue Shirt",
                    11,
                    new List<string> { "small" },
                    "This shirt perfectly pairs with a red hat."
                ),
                new(
                    "A Red Shirt",
                    12,
                    new List<string> { "medium" },
                    "This shirt perfectly pairs with a green hat."
                ),
                new(
                    "A Green Shirt",
                    13,
                    new List<string> { "large" },
                    "This shirt perfectly pairs with a blue hat."
                ),
                new(
                    "A Blue Shirt",
                    14,
                    new List<string> { "small", "medium" },
                    "This shirt perfectly pairs with a red hat."
                ),
                new(
                    "A Red Shirt",
                    15,
                    new List<string> { "small", "large" },
                    "This shirt perfectly pairs with a green hat."
                ),
                new(
                    "A Green Shirt",
                    16,
                    new List<string> { "medium", "large" },
                    "This shirt perfectly pairs with a blue hat."
                ),
                new(
                    "A Blue Shirt",
                    17,
                    new List<string>(),
                    "This shirt perfectly pairs with a red hat."
                ),
                new(
                    "A Red Shirt",
                    18,
                    new List<string> { "small", "medium", "large" },
                    "This shirt perfectly pairs with a green bag."
                ),
                new(
                    "A Green Shirt",
                    19,
                    new List<string> { "small" },
                    "This shirt perfectly pairs with a blue bag."
                ),
                new(
                    "A Blue Shirt",
                    20,
                    new List<string> { "medium" },
                    "This shirt perfectly pairs with a red bag."
                ),
                new(
                    "A Red Shirt",
                    21,
                    new List<string> { "large" },
                    "This shirt perfectly pairs with a green bag."
                ),
                new(
                    "A Green Shirt",
                    22,
                    new List<string> { "small", "medium" },
                    "This shirt perfectly pairs with a blue bag."
                ),
                new(
                    "A Blue Shirt",
                    23,
                    new List<string> { "small", "large" },
                    "This shirt perfectly pairs with a red bag."
                ),
                new(
                    "A Red Shirt",
                    24,
                    new List<string> { "medium", "large" },
                    "This shirt perfectly pairs with a green bag."
                ),
                new(
                    "A Green Shirt",
                    25,
                    new List<string>(),
                    "This shirt perfectly pairs with a blue bag."
                ),
                new(
                    "A Blue Hat",
                    10,
                    new List<string> { "small", "medium", "large" },
                    "This hat perfectly pairs with a red shoe."
                ),
                new(
                    "A Red Hat",
                    11,
                    new List<string> { "small" },
                    "This hat perfectly pairs with a green shoe."
                ),
                new(
                    "A Green Hat",
                    12,
                    new List<string> { "medium" },
                    "This hat perfectly pairs with a blue shoe."
                ),
                new(
                    "A Blue Hat",
                    13,
                    new List<string> { "large" },
                    "This hat perfectly pairs with a red shoe."
                ),
                new(
                    "A Red Hat",
                    14,
                    new List<string> { "small", "medium" },
                    "This hat perfectly pairs with a green shoe."
                ),
                new(
                    "A Green Hat",
                    15,
                    new List<string> { "small", "large" },
                    "This hat perfectly pairs with a blue shoe."
                ),
                new(
                    "A Blue Hat",
                    16,
                    new List<string> { "medium", "large" },
                    "This hat perfectly pairs with a red shoe."
                ),
                new(
                    "A Red Hat",
                    17,
                    new List<string>(),
                    "This hat perfectly pairs with a green shoe."
                ),
                new(
                    "A Green Hat",
                    18,
                    new List<string> { "small", "medium", "large" },
                    "This hat perfectly pairs with a blue tie."
                ),
                new(
                    "A Blue Hat",
                    19,
                    new List<string> { "small" },
                    "This hat perfectly pairs with a red tie."
                ),
                new(
                    "A Red Hat",
                    20,
                    new List<string> { "medium" },
                    "This hat perfectly pairs with a green tie."
                ),
                new(
                    "A Green Hat",
                    21,
                    new List<string> { "large" },
                    "This hat perfectly pairs with a blue tie."
                ),
                new(
                    "A Blue Hat",
                    22,
                    new List<string> { "small", "medium" },
                    "This hat perfectly pairs with a red tie."
                ),
                new(
                    "A Red Hat",
                    23,
                    new List<string> { "small", "large" },
                    "This hat perfectly pairs with a green tie."
                ),
                new(
                    "A Green Hat",
                    24,
                    new List<string> { "medium", "large" },
                    "This hat perfectly pairs with a blue tie."
                ),
                new(
                    "A Blue Hat",
                    25,
                    new List<string>(),
                    "This hat perfectly pairs with a red tie."
                )
            },
            new ApiKeys("0c4bbda1-bf7b-479d-b619-83a1df21f4e7", "a909ff08-d41b-4995-b2af-7b3efbdba597")
        );

        _productService.GetProductsAsync().Returns(productResponse);
    }

    internal GetProductsQueryHandler Sut { get; }
}
