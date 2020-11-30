using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.coin.Models
{
    public record DashbordViewModel(List<ExternalAccount> Accounts, string Token, string TokenPayload);

    public enum AccountType { Savings, Checking, Mortgage }

    public record ExternalAccount(string AccountName, decimal Balance, AccountType AccountType);
}
