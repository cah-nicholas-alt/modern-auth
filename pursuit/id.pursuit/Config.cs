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
                new ApiScope(Scopes.OpenAccount, "Open New Account"),
                new ApiScope(Scopes.TransferFunds, "Transfer Funds to Another Account"),
                new ApiScope(Scopes.ReadAccounts, "Read Account Details"),
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
                    AllowedScopes = {Scopes.OpenAccount, Scopes.ReadAccounts},
                    AllowedGrantTypes = GrantTypes.ClientCredentials
                }
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new []
            {
                new ApiResource
                {
                    Name = "PursuitAccount.API",
                    DisplayName = "Pursuit Account API",
                    Scopes = new List<string>
                    {
                        Scopes.OpenAccount,
                        Scopes.ReadAccounts,
                        Scopes.TransferFunds
                    }
                },
                new ApiResource
                {
                    Name = "PursuitMortgage.API",
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
        public const string ReadAccounts = "PursuitAccountApi.ReadAccounts";
        public const string TransferFunds = "PursuitAccountApi.TransferFunds";
        public const string OpenAccount = "PursuitAccountApi.OpenAccounts";
    }
}