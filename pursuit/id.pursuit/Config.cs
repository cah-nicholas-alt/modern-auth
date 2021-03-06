﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Linq;

namespace id.pursuit
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
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
                    AllowedScopes = {
                        Scopes.ApplyMortgage,
                        Scopes.ReadMortgage,
                        Scopes.ReadAccounts,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    AllowedGrantTypes = new List<string>{ "authorization_code", "password" },
                    RequireClientSecret = false,
                    RedirectUris =
                    {
                        "http://mortgage.app.pursuit.local:5005/signin-oidc"
                    },
                    PostLogoutRedirectUris = { "http://mortgage.app.pursuit.local:5005/signout-callback-oidc" },
                    FrontChannelLogoutUri = "http://mortgage.app.pursuit.local:5005/home/frontchannellogout"
                },
                new Client
                {
                    ClientId = "Pursuit.Accounts.App",
                    AllowedScopes =
                    {
                        Scopes.ReadAccounts,
                        Scopes.AccountAdmin,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    AllowedGrantTypes = new List<string>() {"password", "implicit" },
                    RequireClientSecret = false,
                    RedirectUris =
                    {
                        "http://accounts.app.pursuit.local:5003/LoginCallback"
                    },
                    PostLogoutRedirectUris = { "http://accounts.app.pursuit.local:5003" },
                    AllowAccessTokensViaBrowser = true,
                },
                new Client
                {
                    ClientId = "Coin.App",
                    ClientName = "Coin",
                    ClientSecrets =
                    {
                        new Secret("securePassword!".Sha256())
                    },
                    RedirectUris =
                    {
                        "http://app.coin.local:5004/dashboard/linkaccountcallback"
                    },
                    AllowedScopes = {Scopes.ReadAccounts },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = true,
                    RequirePkce = false,
                    RequireConsent = true
                }
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource
                {
                    Name = "http://accounts.api.pursuit.local:5001/",
                    DisplayName = "Pursuit Account API",
                    Scopes = new List<string>
                    {
                        Scopes.ReadAccounts,
                        Scopes.AccountAdmin
                    }
                },
                new ApiResource
                {
                    Name = "http://mortgage.api.pursuit.local:5002/",
                    DisplayName = "Pursuit Mortgage API",
                    Scopes = new List<string>
                    {
                        Scopes.ApplyMortgage,
                        Scopes.ReadMortgage
                    }
                }
            };

        public static List<TestUser> GetUsers() =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "33164480B55C461691A6EA458C2AC614",
                    Username = "alice",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "483B25A568074E8F8CBA23AE18F4086E",
                    Username = "bob",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "0860256F8C5A466F879D1644919B2DE8",
                    Username = "chris",
                    Password = "password"
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