using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using WebAPI_BackOffice.DB;

namespace WebAPI_BackOffice
{
    //public class CustomAuthorize: IAuthorizationFilter
    //{
    //    private readonly IHttpContextAccessor _httpContextAccessor;
    //    //public void OnAuthorization(AuthorizationFilterContext context)
    //    //{
    //    //    if (context != null)
    //    //    {
    //    //        // Auth logic
    //    //    }
    //    //} 
    //    public CustomAuthorize(IHttpContextAccessor httpContextAccessor)
    //    {
    //        _httpContextAccessor = httpContextAccessor.HttpContext.User.Claims;
    //    }
    //}
    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthorizeFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            OktaContext _oktaContext = new OktaContext();
            if (!_httpContextAccessor.HttpContext!.Request.Headers["FRNSW_OKTA_TOKEN"].Any())
            {
                context.Result = new UnauthorizedObjectResult(string.Empty);
                return;
            }
            else
            {
                var results = _oktaContext.ValidateOktaToken(_httpContextAccessor.HttpContext!.Request.Headers["FRNSW_OKTA_TOKEN"].ToString()).Result;
                if (results.active != true)
                {
                    context.Result = new UnauthorizedObjectResult("Token is expired or invalid");
                    return;
                }
            }
        }
    }
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute() : base(typeof(CustomAuthorizeFilter))
        {
        }
    }
}
