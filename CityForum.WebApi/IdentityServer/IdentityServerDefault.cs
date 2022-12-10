using Duende.IdentityServer.Models;

namespace CityForum.WebApi.IdentityServer;

public static class IdentityServerDefaults
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("api","Access to WebAPI")
        };

    public static IEnumerable<Client> Clients =>
    new List<Client>
    {
        new Client
        {
            ClientId = "desktop",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },
            AllowedScopes = {"api"}
        },
        new Client
        {
            ClientId = "swagger",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            ClientSecrets =
            {
                new Secret("swagger".Sha256())
            },
            AllowedScopes = {"api"}
        }
    };
}