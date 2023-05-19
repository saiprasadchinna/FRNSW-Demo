using IdentityModel.Client;
using IdentityModel.OidcClient.Browser;

namespace MAUI_Demo;

public class WebViewBrowserAuthenticator : IdentityModel.OidcClient.Browser.IBrowser
{
    private WebView _webView;

    public WebViewBrowserAuthenticator(WebView webView)
    {
        _webView = webView;
    }

    public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
    {
        //options.StartUrl = options.StartUrl.Replace("/oauth2/v1/authorize", "/oauth2/default/v1/authorize");
        //_webView = new WebView();

        //WebAuthenticatorResult result2 = await WebAuthenticator.AuthenticateAsync(new Uri(options.StartUrl), new Uri(options.EndUrl));


        //WebAuthenticatorResult result = await WebAuthenticator.Default.AuthenticateAsync(
        // new Uri(options.StartUrl),
        // new Uri(options.EndUrl));

        //var url = new RequestUrl(options.EndUrl)
        //    .Create(new Parameters(result.Properties));

        //return new BrowserResult
        //{
        //    Response = url,
        //    ResultType = BrowserResultType.Success
        //};

        var tcs = new TaskCompletionSource<BrowserResult>();
        _webView.Navigated += (sender, e) =>
        {
            if (e.Url.StartsWith(options.EndUrl))
            {
                _webView.WidthRequest = 0;
                _webView.HeightRequest = 0;
                if (tcs.Task.Status != TaskStatus.RanToCompletion)
                {
                    tcs.SetResult(new BrowserResult
                    {
                        ResultType = BrowserResultType.Success,
                        Response = e.Url.ToString()
                    });
                }
                 
            }

        };

        _webView.WidthRequest = 600;
        _webView.HeightRequest = 600;
        var cnt = _webView.Cookies?.Count;
        _webView.Source = new UrlWebViewSource { Url = options.StartUrl };

        return await tcs.Task;
    }
}
