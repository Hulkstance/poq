﻿using FluentAssertions;
using Poq.ProductService.Application.Queries.GetProducts;
using Xunit;

namespace Poq.ProductService.Tests.Unit;

public class GetProductsQueryValidatorTests
{
    private readonly GetProductsQueryValidator _validator;

    public GetProductsQueryValidatorTests()
    {
        _validator = new GetProductsQueryValidator();
    }

    [Theory]
    [InlineData("large")]
    [InlineData("small", "medium")]
    public async Task Validate_ShouldBeValid_WhenGivenValidInput(params string[] sizes)
    {
        // Arrange
        var query = new GetProductsQuery(Size: sizes);

        // Act
        var actual = await _validator.ValidateAsync(query);

        // Assert
        actual.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_ShouldBeValid_WhenGivenInvalidInput()
    {
        // Arrange
        var size = new[] { "not existing size" };
        var query = new GetProductsQuery(Size: size);

        // Act
        var actual = await _validator.ValidateAsync(query);

        // Assert
        actual.IsValid.Should().BeFalse();
    }
}