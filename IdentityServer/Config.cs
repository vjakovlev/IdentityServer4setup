using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public static class Config
    {
        //to identity token
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "viktor.scope",
                    UserClaims = 
                    { 
                        "cl.viktor"
                    }
                }
            };
        }

        //to access token
        public static IEnumerable<ApiResource> GetApis() 
        {
            return new List<ApiResource>
            {
                new ApiResource("ApiOne"),
                new ApiResource("ApiTwo", new string[] { "cl.api.viktor" })
            };
        }

        public static IEnumerable<Client> GetClients() 
        {
            return new List<Client>
            {
                //api to api backchanel communication
                new Client
                {
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("viktor".ToSha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    AllowedScopes = { "ApiOne" }
                },

                new Client
                {
                    ClientId = "client_id_mvc",
                    ClientSecrets = { new Secret("viktor_mvc".ToSha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:44700/signin-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "ApiOne",
                        "ApiTwo",
                        "viktor.scope"
                    },
                        
                    //mindless - puts all the claims in the id token
                    //AlwaysIncludeUserClaimsInIdToken = true,

                    AllowOfflineAccess = true,

                    RequireConsent = false
                },

                new Client
                {
                    ClientId = "client_id_js",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    RedirectUris = { "https://localhost:44600/home/signin" },
                    AllowedCorsOrigins = { "https://localhost:44600" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "ApiOne",
                        "ApiTwo",
                        "viktor.scope"
                    },
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false
                }
            };
        }
    }
}
