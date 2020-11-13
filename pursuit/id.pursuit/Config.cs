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
                new ApiScope(Scopes.ReadMortgage, "Read Mortgage Details"),
                new ApiScope(Scopes.ApplyMortgage, "Apply for Mortgage"),
                new ApiScope(Scopes.AccountAdmin, "Open New Accounts and Transfer Funds from Account to Account"),
                new ApiScope(Scopes.ReadAccounts, "Read Account Details"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "Pursuit.Mortgage.Api",
                    ClientSecrets =
                    {
                        new Secret("securePassword!".Sha256())
                    },
                    AllowedScopes = {Scopes.AccountAdmin, Scopes.ReadAccounts},
                    AllowedGrantTypes = GrantTypes.ClientCredentials
                },
                new Client
                {
                    ClientId = "Pursuit.Mortgage.App",
                    ClientSecrets =
                    {
                        new Secret("securePassword!".Sha256())
                    },
                    AllowedScopes = {Scopes.ApplyMortgage, Scopes.ReadMortgage},
                    AllowedGrantTypes = GrantTypes.ClientCredentials
                }
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new []
            {
                new ApiResource
                {
                    Name = "http://accounts.api.pursuit:5001/",
                    DisplayName = "Pursuit Account API",
                    Scopes = new List<string>
                    {
                        Scopes.ReadAccounts,
                        Scopes.AccountAdmin
                    }
                },
                new ApiResource
                {
                    Name = "http://mortgage.api.pursuit:5002/",
                    DisplayName = "Pursuit Mortgage API",
                    Scopes = new List<string>
                    {
                        Scopes.ApplyMortgage,
                        Scopes.ReadMortgage
                    }
                }
            };
    }

    public static class Scopes
    {
        public const string ReadMortgage = "PursuitMortgageApi.ReadMortgage";
        public const string ApplyMortgage = "PursuitMortgageApi.ApplyMortgage";
        public const string ReadAccounts = "PursuitAccountsApi.ReadAccounts";
        public const string AccountAdmin = "PursuitAccountsApi.Admin";
    }
}