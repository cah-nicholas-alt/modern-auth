using System;

namespace accounts.api.pursuit
{
    public enum AccountType { Savings, Checking, Mortgage }

    public class Account
    {
        public Guid AccountId { get; }
        public string AccountName => $"{Users.GetUserName(UserId)}'s {AccountType}";
        public AccountType AccountType { get; }
        public decimal Balance { get; set; }
        public Guid UserId { get; }

        public Account(Guid userId, AccountType accountType, decimal balance)
        {
            UserId = userId;
            AccountType = accountType;
            Balance = balance;
            AccountId = Guid.NewGuid();
        }

    }
}
