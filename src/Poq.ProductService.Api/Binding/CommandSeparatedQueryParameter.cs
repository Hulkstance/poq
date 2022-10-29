namespace Poq.ProductService.Api.Binding;

// Minimal API Custom binding: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0#custom-binding
internal sealed class CommandSeparatedQueryParameter
{
    public string[]? Value { get; init; }

    public static bool TryParse(string? value, out CommandSeparatedQueryParameter result)
    {
        result = new CommandSeparatedQueryParameter
        {
            Value = value?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>()
        };

        return true;
    }
}
