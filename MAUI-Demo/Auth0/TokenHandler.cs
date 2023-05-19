//using Microsoft.AspNetCore.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_Demo.Auth0
{
    public class TokenHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", TokenHolder.AccessToken);

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
