using IdentityModel.OidcClient.Results;
using MauiAuth0App.Auth0;
using Microsoft.AspNetCore.Mvc;
using WebAPI_BackOffice.DB;
using WebAPI_BackOffice.Models;

namespace WebAPI_BackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OktaController : Controller
    {

        [HttpGet("ValidateOktaTokenFromApp")]
        [CustomAuthorize]
        public ApiResponse validOktaToken()
        {
            return new ApiResponse("Successfully Validated Okta Token");
        }
    }
}
