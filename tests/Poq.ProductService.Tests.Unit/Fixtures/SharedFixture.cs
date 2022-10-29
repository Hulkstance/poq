using Poq.ProductService.Application.Queries.GetProducts;

namespace Poq.ProductService.Tests.Unit.Fixtures;

// When you want to create a single test context and share it among all the tests in the class,
// and have it cleaned up after all the tests in the class have finished.
public sealed class SharedFixture
{
    public SharedFixture()
    {
        QueryHandler = new GetProductsQueryHandler(MockData.GetProducts().Object);
    }

    public GetProductsQueryHandler QueryHandler { get; }
}
