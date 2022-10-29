using FluentValidation;

namespace Poq.ProductService.Application.Queries.GetProducts;

public sealed class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        var conditions = new List<string> { "small", "medium", "large" };

        RuleFor(x => x.Size)
            .ForEach(i => i
                .Must(s => conditions.Contains(s!))
                .WithMessage($"Please only use: {string.Join(",", conditions)}")
                .When(s => s != null)
            )
            .When(x => x.Size != null);
    }
}
