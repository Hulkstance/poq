using System.Diagnostics.CodeAnalysis;

namespace Poq.ProductService.Api.Binding;

internal class CommaSeparatedQueryParam
{
    public string[] Value { get; private init; } = Array.Empty<string>();

    public static bool TryParse(string? value, [NotNullWhen(true)] out CommaSeparatedQueryParam? filter)
    {
        try
        {
            var splitValue = value?.Split(',').ToArray();
            filter = new CommaSeparatedQueryParam
            {
                Value = splitValue!
            };
            return true;
        }
        catch
        {
            filter = default;
            return false;
        }
    }
}
