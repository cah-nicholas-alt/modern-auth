using System;

namespace accounts.api.pursuit
{
    public enum AccountType { Savings, Mortgage}
    
    public record Account(Guid UserId, AccountType AccountType, decimal Balance);
}
