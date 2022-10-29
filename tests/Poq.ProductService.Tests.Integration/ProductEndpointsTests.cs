using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Poq.ProductService.Application.Models;
using Xunit;

namespace Poq.ProductService.Tests.Integration;

public class ProductEndpointsTests : IClassFixture<ProductServiceApiFactory>
{
    private readonly HttpClient _client;
    private readonly MockyApiServer _mockyApiServer;

    public ProductEndpointsTests(ProductServiceApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost:5001")
        });
        _mockyApiServer = apiFactory.MockyApiServer;
    }

    [Fact]
    public async Task GetProducts_ShouldReturnStatusOkAndAllProducts_WhenParametersAreNotGiven()
    {
        // Arrange
        _mockyApiServer.SetupProducts();

        // Act
        var response = await _client.GetAsync("/api/v1/product");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var productResponse = await response.Content.ReadFromJsonAsync<Response>();
        productResponse?.Products.Should().NotBeEmpty().And.HaveCount(48);
    }

    [Fact]
    public async Task GetProducts_ShouldReturnStatusOkAndFilteredSubsetOfProducts_WhenAllParametersAreGiven()
    {
        // Arrange
        const double minPrice = 10;
        const double maxPrice = 14;
        const string size = "medium,large";
        const string highlight = "red,blue";
        _mockyApiServer.SetupProducts();

        // Act
        var response = await _client.GetAsync($"/api/v1/product?minPrice={minPrice}&maxPrice={maxPrice}&size={size}&highlight={highlight}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var productResponse = await response.Content.ReadFromJsonAsync<Response>();
        productResponse?.Products.Should().HaveCount(12);
        productResponse?.Products.Should().AllSatisfy(p =>
        {
            p.Price.Should().BePositive();
            p.Price.Should().BeGreaterThanOrEqualTo(minPrice).And.BeLessThanOrEqualTo(maxPrice);
            p.Sizes.Should().NotBeEmpty();
            p.Sizes.Should().IntersectWith(size.Split(','));
        });
        productResponse?.Products
            .Where(p => highlight.Split(',').Any(k => p.Description.Contains(k)))
            .Should().AllSatisfy(p =>
                p.Description.Should().MatchRegex(@"<\s*([^ >]+)[^>]*>.*?<\s*\/\s*\1\s*>"));
    }

    [Fact]
    public async Task GetProducts_ShouldReturnBadRequestAndEmptyProductsList_WhenGivenNegativeMaxPrice()
    {
        // Arrange
        const double maxPrice = -1;

        _mockyApiServer.SetupProducts();

        // Act
        var response = await _client.GetAsync($"/api/v1/product?&maxPrice={maxPrice}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var productResponse = await response.Content.ReadFromJsonAsync<Response>();
        productResponse?.Success.Should().BeFalse();
        productResponse?.Products.Should().HaveCount(0);
    }

    [Fact]
    public async Task GetProducts_ShouldReturnStatusBadRequestAndErrorMessage_WhenGivenNotExistingSize()
    {
        // Arrange
        const string size = "not existing size";

        _mockyApiServer.SetupProducts();

        // Act
        var response = await _client.GetAsync($"/api/v1/product?&size={size}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var productResponse = await response.Content.ReadFromJsonAsync<Response>();
        productResponse?.Success.Should().BeFalse();
        productResponse?.Message.Should().Be("Validation failed: \r\n -- Size[0]: Please only use: small,medium,large Severity: Error");
    }
}
