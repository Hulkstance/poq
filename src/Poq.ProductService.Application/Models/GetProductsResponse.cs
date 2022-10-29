using System.Text.Json.Serialization;

namespace Poq.ProductService.Application.Models;

public record GetProductsResponse
(
    [property: JsonPropertyName("products")] IEnumerable<Product> Products,
    [property: JsonPropertyName("apiKeys")] ApiKeys ApiKeys
);

public record ApiKeys
(
    [property: JsonPropertyName("primary")] string Primary,
    [property: JsonPropertyName("secondary")] string Secondary
);
