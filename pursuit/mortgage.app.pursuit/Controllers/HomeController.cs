using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mortgage.app.pursuit.Models;
using System.Net.Http.Json;

namespace mortgage.app.pursuit.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            HomeViewModel viewModel = null;
            if (User.Identity.IsAuthenticated)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var mortgages = await client.GetFromJsonAsync<List<Mortgage>>("http://mortgage.api.pursuit.local:5002/api/mortgages");
                var accounts = await client.GetFromJsonAsync<List<Account>>("http://accounts.api.pursuit.local:5001/api/accounts");

                for(int i = 0; i < mortgages.Count; i++)
                {
                    var balance = accounts.FirstOrDefault(a => a.AccountId == mortgages[i].AccountId)
                        ?.Balance ?? 0m;

                    mortgages[i] = mortgages[i] with { Balance = balance };
                }

                viewModel = new HomeViewModel(mortgages);
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Apply(HomeViewModel viewModel)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            await client.PostAsJsonAsync<MortgageApplication>("http://mortgage.api.pursuit.local:5002/api/mortgages/apply", viewModel.Application);
            return RedirectToAction("Index");
        }

        public IActionResult Token()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Login()
        {
            return RedirectToAction("Index");
        }

        [HttpGet("Home/Login")]
        public IActionResult LoginCallback()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        public async Task<IActionResult> FrontChannelLogout(string sid, string iss)
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentSid = User.Claims.FirstOrDefault(c => c.Type == "sid")?.Value ?? "";
                if (string.Equals(currentSid, sid, StringComparison.Ordinal))
                {
                    await HttpContext.SignOutAsync();
                }
            }
            return new EmptyResult();
        }
    }
}
