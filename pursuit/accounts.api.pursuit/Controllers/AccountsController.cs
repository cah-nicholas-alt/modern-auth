using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace accounts.api.pursuit.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public partial class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private Guid UserId => Guid.Parse(User?.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

        public AccountsController(ILogger<AccountsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return AccountRepo.Accounts.Where(a =>
                a.UserId == UserId);
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public IEnumerable GetAccountList()
        {
            return AccountRepo.Accounts.Select(a =>
                new
                {
                    a.AccountId,
                    a.AccountName,
                    a.AccountType,
                    a.UserId
                });
        }

        [HttpPost("transfer")]
        public void SendFunds(SendFundsRequest request)
        {
            var (sourceAccount, targetAccount, amount) = request;

            if (amount < 0)
                throw new Exception("Invalid amount, amount must be greater than 0");

            var source = AccountRepo.Accounts.FirstOrDefault(a =>
                a.AccountId == sourceAccount && a.UserId == UserId);

            if (source == null)
                throw new Exception("User does not own source account");

            var target = AccountRepo.Accounts.First(a => a.AccountId == targetAccount);

            target.Balance += amount;
            source.Balance -= amount;
        }

        [HttpPost()]
        public void CreateAccount(Account account)
        {
            AccountRepo.Accounts.Add(account);
        }
    }
}
