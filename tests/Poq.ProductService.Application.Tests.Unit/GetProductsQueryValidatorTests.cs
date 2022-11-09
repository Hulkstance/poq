using FluentAssertions;
using Poq.ProductService.Application.Queries.GetProducts;
using Xunit;

namespace Poq.ProductService.Application.Tests.Unit;

public class GetProductsQueryValidatorTests
{
    private readonly GetProductsQueryValidator _sut = new();

    [Theory]
    [InlineData("large")]
    [InlineData("small", "medium")]
    public async Task Validate_ShouldBeValid_WhenValidInputIsGiven(params string[] sizes)
    {
        // Arrange
        var query = new GetProductsQuery(Size: sizes);

        // Act
        var actual = await _sut.ValidateAsync(query);

        // Assert
        actual.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_ShouldBeValid_WhenInvalidInputIsGiven()
    {
        // Arrange
        var size = new[] { "not existing size" };
        var query = new GetProductsQuery(Size: size);

        // Act
        var actual = await _sut.ValidateAsync(query);

        // Assert
        actual.IsValid.Should().BeFalse();
    }
}
