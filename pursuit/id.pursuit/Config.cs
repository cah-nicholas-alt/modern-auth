// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace id.pursuit
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(Scopes.ReadMortgage), 
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "Pursuit.Mortgage",
                    ClientSecrets =
                    {
                        new Secret("mortgage.123".Sha256())
                    },
                    AllowedScopes = {Scopes.ReadMortgage},
                    AllowedGrantTypes = GrantTypes.ClientCredentials
                }
            };
    }

    public static class Scopes
    {
        public const string ReadMortgage = "PursuitMortgageApi.ReadMortgage";
    }
}