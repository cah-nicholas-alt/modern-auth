using System.Collections.Generic;

namespace accounts.api.pursuit
{
    public class AccountRepo
    {
        public static List<Account> Accounts
            = new List<Account>
            {
                new Account(Users.Alice, AccountType.Savings, 1000),
                new Account(Users.Bob, AccountType.Savings, 1000),
                new Account(Users.Bob, AccountType.Checking, 1000),
                new Account(Users.Chris, AccountType.Savings, 1000),
                new Account(Users.Chris, AccountType.Mortgage, -200000),
            };
    }
}
