using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mortgage.app.pursuit.Models
{
    public enum AccountType { Savings, Checking, Mortgage }

    public record Account(Guid AccountId, decimal Balance, AccountType AccountType);
}
