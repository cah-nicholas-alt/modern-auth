using app.coin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace app.coin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            TokenRepo.Tokens.TryGetValue(User.Identity.Name, out var t);

            var vm = new DashbordViewModel(new List<ExternalAccount>(), "", "");

            if (!string.IsNullOrEmpty(t.token))
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", t.token);
                var resp = await client.GetFromJsonAsync<List<ExternalAccount>>("http://accounts.api.pursuit.local:5001/api/accounts");

                vm = new DashbordViewModel(resp, t.token, t.payload);
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult LinkAccount()
        {
            var clientId = "Coin.App";
            var scope = "PursuitAccountsApi.ReadAccounts";
            var redirect = "http://app.coin.local:5004/dashboard/linkaccountcallback";
            var responseType = "code";

            return Redirect($"http://id.pursuit.local:5000/connect/authorize?client_id={clientId}&scope={scope}&response_type={responseType}&redirect_uri={redirect}");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> LinkAccountCallback(string code)
        {
            var clientId = "Coin.App";
            var clientSecret = "securePassword!";
            var redirect = "http://app.coin.local:5004/dashboard/linkaccountcallback";

            var response = await new HttpClient().PostAsync("http://id.pursuit.local:5000/connect/token",
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "client_id", clientId },
                    { "client_secret", clientSecret},
                    { "grant_type", "authorization_code" },
                    { "code", code },
                    { "redirect_uri", redirect}
                }));

            string resp = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<JObject>(resp)["access_token"].ToString();
            var payload64 = token.Split(".")[1];
            payload64 = payload64.PadRight(payload64.Length + (payload64.Length % 4), '=');
            var payload = Encoding.UTF8.GetString(Convert.FromBase64String(payload64));
            JToken jt = JToken.Parse(payload);
            payload = jt.ToString(Newtonsoft.Json.Formatting.Indented);

            TokenRepo.Tokens[User.Identity.Name] = (token, payload);

            return RedirectToAction("Index");
        }
    }

    public class TokenRepo
    {
        public static Dictionary<string, (string token, string payload)> Tokens = new Dictionary<string, (string, string)>();
    }
}
