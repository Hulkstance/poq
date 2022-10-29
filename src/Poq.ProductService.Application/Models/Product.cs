using System.Text.Json.Serialization;

namespace Poq.ProductService.Application.Models;

public record Product(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("price")] double Price,
    [property: JsonPropertyName("sizes")] List<string> Sizes,
    [property: JsonPropertyName("description")] string Description);
