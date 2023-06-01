using IdentityModel.OidcClient.Results;
using MauiAuth0App.Auth0;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_BackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OktaController : Controller
    {
        private readonly Auth0Client auth0Client;
        public IActionResult Index()
        {
            return View();
        }

        private async Task<UserInfoResult> LogOutUser(string? accessToken)
        {
            var results = await auth0Client.getUserInfo(accessToken);
            return results;
        }
    }
}
