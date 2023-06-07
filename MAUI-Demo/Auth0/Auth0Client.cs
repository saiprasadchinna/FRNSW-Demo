﻿using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using IdentityModel.Client;
using IdentityModel.OidcClient.Infrastructure;
using IdentityModel;
using Microsoft.Extensions.Logging;
using static IdentityModel.OidcConstants;
using IdentityModel.OidcClient.Results;

namespace MAUI_Demo.Auth0;

public class Auth0Client
{
    private readonly OidcClient oidcClient;
    private string audience;

    public Auth0Client(Auth0ClientOptions options)
    {
        oidcClient = new OidcClient(new OidcClientOptions
        {
            Authority = $"https://{options.Domain}",
            ClientId = options.ClientId,
            ClientSecret=options.ClientSecret,
            Scope = options.Scope,
            RedirectUri = options.RedirectUri,
            Browser = options.Browser
        });
        audience = options.Audience;
    }

    public IdentityModel.OidcClient.Browser.IBrowser Browser
    {
        get
        {
            return oidcClient.Options.Browser;
        }
        set
        {
            oidcClient.Options.Browser = value;
        }
    }

    public async Task<LoginResult> LoginAsync()
    {

        LoginRequest loginRequest = null;

        if (!string.IsNullOrEmpty(audience))
        {
            loginRequest = new LoginRequest
            {
                FrontChannelExtraParameters = new Parameters(new Dictionary<string, string>()
            {
              {"audience", audience}
            })
            };
        }

        //var discoveryClient = oidcClient.CreateClient();
        return await oidcClient.LoginAsync(loginRequest);
    }

    public async Task<BrowserResult> LogoutAsync()
    {
        var logoutParameters = new Dictionary<string, string>
    {
      {"client_id", oidcClient.Options.ClientId },
      {"returnTo", oidcClient.Options.RedirectUri }
    };

        var logoutRequest = new LogoutRequest();
        var endSessionUrl = new RequestUrl($"{oidcClient.Options.Authority}/v2/logout")
          .Create(new Parameters(logoutParameters));
        var browserOptions = new BrowserOptions(endSessionUrl, oidcClient.Options.RedirectUri)
        {
            Timeout = TimeSpan.FromSeconds(logoutRequest.BrowserTimeout),
            DisplayMode = logoutRequest.BrowserDisplayMode
        };

        var browserResult = await oidcClient.Options.Browser.InvokeAsync(browserOptions);

        return browserResult;
    }

    public async Task<BrowserResult> LogoutOktaAsync()
    {
        string oauthToken = await SecureStorage.Default.GetAsync("oauth_token");
        var logoutParameters = new Dictionary<string, string>
    {
      {"id_token_hint", oauthToken },
    };

        var logoutRequest = new LogoutRequest();
        var endSessionUrl = new RequestUrl($"{oidcClient.Options.Authority}/oauth2/v1/logout")
          .Create(new Parameters(logoutParameters));
        var browserOptions = new BrowserOptions(endSessionUrl, oidcClient.Options.RedirectUri)
        {
            Timeout = TimeSpan.FromSeconds(logoutRequest.BrowserTimeout),
            DisplayMode = logoutRequest.BrowserDisplayMode
        };

        var browserResult = await oidcClient.Options.Browser.InvokeAsync(browserOptions);

        return browserResult;
    }
    public async Task<LogoutResult> CustomLogoutAsync()
    {
        //await oidcClient.LogoutAsync();
        string IdToken = TokenHolder.IdentityToken;
        ////var response = await oidcClient.LogoutAsync(new LogoutRequest());
        //LogoutResult logoutResult = await oidcClient.LogoutAsync(new LogoutRequest { IdTokenHint = oauthToken });

        var logoutRequest = new LogoutRequest
        {
            IdTokenHint = IdToken,
            BrowserDisplayMode = DisplayMode.Visible
        };
        LogoutResult logoutResult = await oidcClient.LogoutAsync(logoutRequest);

        return logoutResult;
    }

    public async Task<UserInfoResult> getUserInfo(string accessToken)
    {
        return await oidcClient.GetUserInfoAsync(accessToken);
    }

}