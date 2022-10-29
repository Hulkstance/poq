using FluentAssertions;
using Poq.ProductService.Application.Queries.GetProducts;
using Poq.ProductService.Tests.Unit.Fixtures;
using Xunit;

namespace Poq.ProductService.Tests.Unit;

public class GetProductsQueryTests : IClassFixture<SharedFixture>
{
    private readonly SharedFixture _sharedFixture;

    public GetProductsQueryTests(SharedFixture sharedFixture)
    {
        _sharedFixture = sharedFixture;
    }

    [Fact]
    public async Task Handle_ShouldReturnAllProducts_WhenParametersAreNotGiven()
    {
        // Arrange
        var sut = new GetProductsQuery();

        // Act
        var actual = await _sharedFixture.QueryHandler.Handle(sut, default);

        // Assert
        actual.Products.Should().NotBeEmpty().And.HaveCount(48);
    }

    [Theory]
    [InlineData(10, 14)]
    [InlineData(22, 24)]
    public async Task Handle_ShouldReturnFilteredSubsetOfProducts_WhenGivenMinPriceAndMaxPrice(double minPrice, double maxPrice)
    {
        // Arrange
        var sut = new GetProductsQuery(minPrice, maxPrice);

        // Act
        var actual = await _sharedFixture.QueryHandler.Handle(sut, default);

        // Assert
        actual.Products.Should().AllSatisfy(p =>
        {
            p.Price.Should().BePositive();
            p.Price.Should().BeGreaterThanOrEqualTo(minPrice).And.BeLessThanOrEqualTo(maxPrice);
        });
    }

    [Theory]
    [InlineData("small")]
    [InlineData("medium", "large")]
    public async Task Handle_ShouldReturnFilteredSubsetOfProducts_WhenGivenSize(params string[] sizes)
    {
        // Arrange
        var sut = new GetProductsQuery(Size: sizes);

        // Act
        var actual = await _sharedFixture.QueryHandler.Handle(sut, default);

        // Assert
        actual.Products.Should().AllSatisfy(p =>
        {
            p.Sizes.Should().NotBeEmpty();
            p.Sizes.Should().IntersectWith(sizes);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenGivenNegativeMaxPrice()
    {
        // Arrange
        const double maxPrice = -5;

        var sut = new GetProductsQuery(MaxPrice: maxPrice);

        // Act
        var func = () => _sharedFixture.QueryHandler.Handle(sut, default);

        // Assert
        await func.Should().ThrowAsync<ArgumentOutOfRangeException>()
            .WithMessage("Please provide a valid max price (Parameter 'maxPrice')");
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenGivenNotExistingSize()
    {
        // Arrange
        var sizes = new[] { "not existing size" };

        var sut = new GetProductsQuery(Size: sizes);

        // Act
        var actual = await _sharedFixture.QueryHandler.Handle(sut, default);

        // Assert
        actual.Products.Should().BeEmpty();
    }

    [Theory]
    [InlineData("blue")]
    [InlineData("red", "blue")]
    public async Task Handle_ShouldReturnFilteredHighlightedDescriptions_WhenGivenKeywords(params string[] keywords)
    {
        // Arrange
        var sut = new GetProductsQuery(Highlight: keywords);

        // Act
        var actual = await _sharedFixture.QueryHandler.Handle(sut, default);

        // Assert
        actual.Products
            .Where(p => keywords.Any(k => p.Description.Contains(k)))
            .Should().AllSatisfy(p =>
                p.Description.Should().MatchRegex(@"<\s*([^ >]+)[^>]*>.*?<\s*\/\s*\1\s*>"));
    }

    [Fact]
    public async Task Handle_ShouldReturnFilteredHighlightedDescriptions_WhenGivenEmptyInput()
    {
        // Arrange
        var sut = new GetProductsQuery();

        // Act
        var actual = await _sharedFixture.QueryHandler.Handle(sut, default);

        // Assert
        actual.Products.Should().AllSatisfy(p =>
            p.Description.Should().NotMatchRegex(@"<\s*([^ >]+)[^>]*>.*?<\s*\/\s*\1\s*>"));
    }

    [Fact]
    public async Task Handle_ShouldReturnFilteredHighlightedDescriptions_WhenGivenInvalidInput()
    {
        // Arrange
        var sut = new GetProductsQuery(Highlight: new [] { "not existing word" });

        // Act
        var actual = await _sharedFixture.QueryHandler.Handle(sut, default);

        // Assert
        actual.Products.Should().AllSatisfy(p =>
            p.Description.Should().NotMatchRegex(@"<\s*([^ >]+)[^>]*>.*?<\s*\/\s*\1\s*>"));
    }

    [Fact]
    public async Task Handle_ShouldReturnMostCommonWords_WhenGivenEmptyInput()
    {
        // Arrange
        var sut = new GetProductsQuery();

        var expected = new[]
        {
            "shirt", "hat", "trouser", "green", "blue", "red", "bag", "shoe", "tie", "belt"
        };

        // Act
        var actual = await _sharedFixture.QueryHandler.Handle(sut, default);

        // Assert
        actual.CommonWords.Should().BeEquivalentTo(expected);
    }
}
