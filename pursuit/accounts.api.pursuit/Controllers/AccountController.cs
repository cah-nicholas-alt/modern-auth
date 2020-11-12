using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace accounts.api.pursuit.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Account> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index =>
                new Account(Guid.NewGuid(), AccountType.Mortgage, index));
        }
    }
}
