using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poq.ProductService.Api.Binding;
using Poq.ProductService.Application.Models;
using Poq.ProductService.Application.Queries.GetProducts;

namespace Poq.ProductService.Api.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/product", GetProducts)
            .WithName("GetProducts")
            .CacheOutput(x => x
                .Expire(TimeSpan.FromSeconds(30))
                .Tag("cache"));
    }

    /// <summary>
    /// Gets subset of products.
    /// </summary>
    /// <param name="minPrice">Filters products with a price less than or equal to.</param>
    /// <param name="maxPrice">Filters products with a price greater than or equal to.</param>
    /// <param name="size">The sizes, e.g. <code>small,medium,large</code>.</param>
    /// <param name="highlight">The keywords for the highlighting, e.g. <code>blue,green</code>.</param>
    /// <param name="mediator">The <see cref="IMediator"/> instance.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <response code="200">Returns the subset of products.</response>
    /// <response code="404">If the result is null.</response>
    /// <returns></returns>
    [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    private static async Task<IResult> GetProducts(
        [FromQuery] double? minPrice,
        [FromQuery] double? maxPrice,
        [FromQuery] CommaSeparatedQueryParam? size,
        [FromQuery] CommaSeparatedQueryParam? highlight,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetProductsQuery(minPrice, maxPrice, size?.Value, highlight?.Value);
        var response = await mediator.Send(query, cancellationToken);
        return response.Success
            ? Results.Ok(response)
            : Results.BadRequest();
    }
}
