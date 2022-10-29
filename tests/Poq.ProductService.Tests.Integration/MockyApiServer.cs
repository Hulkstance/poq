using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Poq.ProductService.Tests.Integration;

public sealed class MockyApiServer : IDisposable
{
    private WireMockServer _server;

    public string Url => _server.Url!;

    public void Dispose()
    {
        _server.Stop();
        _server.Dispose();
    }

    public void Start()
    {
        _server = WireMockServer.Start();
    }

    public void SetupProducts()
    {
        _server.Given(Request.Create()
                .WithPath("/v2/5e307edf3200005d00858b49")
                .UsingGet())
            .RespondWith(Response.Create()
                .WithBody(GenerateProductsResponseBody())
                .WithHeader("content-type", "application/json; charset=utf-8")
                .WithStatusCode(200));
    }

    private static string GenerateProductsResponseBody()
    {
        return @"{
    ""products"": [
        {
            ""title"": ""A Red Trouser"",
            ""price"": 10,
            ""sizes"": [
                ""small"",
                ""medium"",
                ""large""
            ],
            ""description"": ""This trouser perfectly pairs with a green shirt.""
        },
        {
            ""title"": ""A Green Trouser"",
            ""price"": 11,
            ""sizes"": [
                ""small""
            ],
            ""description"": ""This trouser perfectly pairs with a blue shirt.""
        },
        {
            ""title"": ""A Blue Trouser"",
            ""price"": 12,
            ""sizes"": [
                ""medium""
            ],
            ""description"": ""This trouser perfectly pairs with a red shirt.""
        },
        {
            ""title"": ""A Red Trouser"",
            ""price"": 13,
            ""sizes"": [
                ""large""
            ],
            ""description"": ""This trouser perfectly pairs with a green shirt.""
        },
        {
            ""title"": ""A Green Trouser"",
            ""price"": 14,
            ""sizes"": [
                ""small"",
                ""medium""
            ],
            ""description"": ""This trouser perfectly pairs with a blue shirt.""
        },
        {
            ""title"": ""A Blue Trouser"",
            ""price"": 15,
            ""sizes"": [
                ""small"",
                ""large""
            ],
            ""description"": ""This trouser perfectly pairs with a red shirt.""
        },
        {
            ""title"": ""A Red Trouser"",
            ""price"": 16,
            ""sizes"": [
                ""medium"",
                ""large""
            ],
            ""description"": ""This trouser perfectly pairs with a green shirt.""
        },
        {
            ""title"": ""A Green Trouser"",
            ""price"": 17,
            ""sizes"": [],
            ""description"": ""This trouser perfectly pairs with a blue shirt.""
        },
        {
            ""title"": ""A Blue Trouser"",
            ""price"": 18,
            ""sizes"": [
                ""small"",
                ""medium"",
                ""large""
            ],
            ""description"": ""This trouser perfectly pairs with a red belt.""
        },
        {
            ""title"": ""A Red Trouser"",
            ""price"": 19,
            ""sizes"": [
                ""small""
            ],
            ""description"": ""This trouser perfectly pairs with a green belt.""
        },
        {
            ""title"": ""A Green Trouser"",
            ""price"": 20,
            ""sizes"": [
                ""medium""
            ],
            ""description"": ""This trouser perfectly pairs with a blue belt.""
        },
        {
            ""title"": ""A Blue Trouser"",
            ""price"": 21,
            ""sizes"": [
                ""large""
            ],
            ""description"": ""This trouser perfectly pairs with a red belt.""
        },
        {
            ""title"": ""A Red Trouser"",
            ""price"": 22,
            ""sizes"": [
                ""small"",
                ""medium""
            ],
            ""description"": ""This trouser perfectly pairs with a green belt.""
        },
        {
            ""title"": ""A Green Trouser"",
            ""price"": 23,
            ""sizes"": [
                ""small"",
                ""large""
            ],
            ""description"": ""This trouser perfectly pairs with a blue belt.""
        },
        {
            ""title"": ""A Blue Trouser"",
            ""price"": 24,
            ""sizes"": [
                ""medium"",
                ""large""
            ],
            ""description"": ""This trouser perfectly pairs with a red belt.""
        },
        {
            ""title"": ""A Red Trouser"",
            ""price"": 25,
            ""sizes"": [],
            ""description"": ""This trouser perfectly pairs with a green belt.""
        },
        {
            ""title"": ""A Green Shirt"",
            ""price"": 10,
            ""sizes"": [
                ""small"",
                ""medium"",
                ""large""
            ],
            ""description"": ""This shirt perfectly pairs with a blue hat.""
        },
        {
            ""title"": ""A Blue Shirt"",
            ""price"": 11,
            ""sizes"": [
                ""small""
            ],
            ""description"": ""This shirt perfectly pairs with a red hat.""
        },
        {
            ""title"": ""A Red Shirt"",
            ""price"": 12,
            ""sizes"": [
                ""medium""
            ],
            ""description"": ""This shirt perfectly pairs with a green hat.""
        },
        {
            ""title"": ""A Green Shirt"",
            ""price"": 13,
            ""sizes"": [
                ""large""
            ],
            ""description"": ""This shirt perfectly pairs with a blue hat.""
        },
        {
            ""title"": ""A Blue Shirt"",
            ""price"": 14,
            ""sizes"": [
                ""small"",
                ""medium""
            ],
            ""description"": ""This shirt perfectly pairs with a red hat.""
        },
        {
            ""title"": ""A Red Shirt"",
            ""price"": 15,
            ""sizes"": [
                ""small"",
                ""large""
            ],
            ""description"": ""This shirt perfectly pairs with a green hat.""
        },
        {
            ""title"": ""A Green Shirt"",
            ""price"": 16,
            ""sizes"": [
                ""medium"",
                ""large""
            ],
            ""description"": ""This shirt perfectly pairs with a blue hat.""
        },
        {
            ""title"": ""A Blue Shirt"",
            ""price"": 17,
            ""sizes"": [],
            ""description"": ""This shirt perfectly pairs with a red hat.""
        },
        {
            ""title"": ""A Red Shirt"",
            ""price"": 18,
            ""sizes"": [
                ""small"",
                ""medium"",
                ""large""
            ],
            ""description"": ""This shirt perfectly pairs with a green bag.""
        },
        {
            ""title"": ""A Green Shirt"",
            ""price"": 19,
            ""sizes"": [
                ""small""
            ],
            ""description"": ""This shirt perfectly pairs with a blue bag.""
        },
        {
            ""title"": ""A Blue Shirt"",
            ""price"": 20,
            ""sizes"": [
                ""medium""
            ],
            ""description"": ""This shirt perfectly pairs with a red bag.""
        },
        {
            ""title"": ""A Red Shirt"",
            ""price"": 21,
            ""sizes"": [
                ""large""
            ],
            ""description"": ""This shirt perfectly pairs with a green bag.""
        },
        {
            ""title"": ""A Green Shirt"",
            ""price"": 22,
            ""sizes"": [
                ""small"",
                ""medium""
            ],
            ""description"": ""This shirt perfectly pairs with a blue bag.""
        },
        {
            ""title"": ""A Blue Shirt"",
            ""price"": 23,
            ""sizes"": [
                ""small"",
                ""large""
            ],
            ""description"": ""This shirt perfectly pairs with a red bag.""
        },
        {
            ""title"": ""A Red Shirt"",
            ""price"": 24,
            ""sizes"": [
                ""medium"",
                ""large""
            ],
            ""description"": ""This shirt perfectly pairs with a green bag.""
        },
        {
            ""title"": ""A Green Shirt"",
            ""price"": 25,
            ""sizes"": [],
            ""description"": ""This shirt perfectly pairs with a blue bag.""
        },
        {
            ""title"": ""A Blue Hat"",
            ""price"": 10,
            ""sizes"": [
                ""small"",
                ""medium"",
                ""large""
            ],
            ""description"": ""This hat perfectly pairs with a red shoe.""
        },
        {
            ""title"": ""A Red Hat"",
            ""price"": 11,
            ""sizes"": [
                ""small""
            ],
            ""description"": ""This hat perfectly pairs with a green shoe.""
        },
        {
            ""title"": ""A Green Hat"",
            ""price"": 12,
            ""sizes"": [
                ""medium""
            ],
            ""description"": ""This hat perfectly pairs with a blue shoe.""
        },
        {
            ""title"": ""A Blue Hat"",
            ""price"": 13,
            ""sizes"": [
                ""large""
            ],
            ""description"": ""This hat perfectly pairs with a red shoe.""
        },
        {
            ""title"": ""A Red Hat"",
            ""price"": 14,
            ""sizes"": [
                ""small"",
                ""medium""
            ],
            ""description"": ""This hat perfectly pairs with a green shoe.""
        },
        {
            ""title"": ""A Green Hat"",
            ""price"": 15,
            ""sizes"": [
                ""small"",
                ""large""
            ],
            ""description"": ""This hat perfectly pairs with a blue shoe.""
        },
        {
            ""title"": ""A Blue Hat"",
            ""price"": 16,
            ""sizes"": [
                ""medium"",
                ""large""
            ],
            ""description"": ""This hat perfectly pairs with a red shoe.""
        },
        {
            ""title"": ""A Red Hat"",
            ""price"": 17,
            ""sizes"": [],
            ""description"": ""This hat perfectly pairs with a green shoe.""
        },
        {
            ""title"": ""A Green Hat"",
            ""price"": 18,
            ""sizes"": [
                ""small"",
                ""medium"",
                ""large""
            ],
            ""description"": ""This hat perfectly pairs with a blue tie.""
        },
        {
            ""title"": ""A Blue Hat"",
            ""price"": 19,
            ""sizes"": [
                ""small""
            ],
            ""description"": ""This hat perfectly pairs with a red tie.""
        },
        {
            ""title"": ""A Red Hat"",
            ""price"": 20,
            ""sizes"": [
                ""medium""
            ],
            ""description"": ""This hat perfectly pairs with a green tie.""
        },
        {
            ""title"": ""A Green Hat"",
            ""price"": 21,
            ""sizes"": [
                ""large""
            ],
            ""description"": ""This hat perfectly pairs with a blue tie.""
        },
        {
            ""title"": ""A Blue Hat"",
            ""price"": 22,
            ""sizes"": [
                ""small"",
                ""medium""
            ],
            ""description"": ""This hat perfectly pairs with a red tie.""
        },
        {
            ""title"": ""A Red Hat"",
            ""price"": 23,
            ""sizes"": [
                ""small"",
                ""large""
            ],
            ""description"": ""This hat perfectly pairs with a green tie.""
        },
        {
            ""title"": ""A Green Hat"",
            ""price"": 24,
            ""sizes"": [
                ""medium"",
                ""large""
            ],
            ""description"": ""This hat perfectly pairs with a blue tie.""
        },
        {
            ""title"": ""A Blue Hat"",
            ""price"": 25,
            ""sizes"": [],
            ""description"": ""This hat perfectly pairs with a red tie.""
        }
    ],
    ""apiKeys"": {
        ""primary"": ""0c4bbda1-bf7b-479d-b619-83a1df21f4e7"",
        ""secondary"": ""a909ff08-d41b-4995-b2af-7b3efbdba597""
    }
}";
    }
}
