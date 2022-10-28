using Duende.IdentityServer.Models;

namespace SimpleStore.Applications.Identity.WebApp
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("internal"),
                new ApiScope("trusted")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets = 
                    { 
                        new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) 
                    },

                    AllowedScopes = 
                    { 
                        "internal", "trusted" 
                    }
                },
                new Client
                {
                    ClientId="backoffice",
                    ClientName = "Backoffice OIDC Client",
                    ClientSecrets = 
                    {
                        new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris =
                    {
                        "http://localhost:60001/signin-oidc",
                        "https://localhost:61001/signin-oidc",
                        "http://backoffice-application:60001/signin-oidc",
                    },
                    PostLogoutRedirectUris=
                    {
                        "http://localhost:60001/signout-callback-oidc",
                        "https://localhost:61001/signout-callback-oidc",
                        "http://backoffice-application:60001/signout-callback-oidc",
                    },

                    AllowedScopes =
                    {
                        "openid",
                        "profile"
                    }
                }
            };
    }
}