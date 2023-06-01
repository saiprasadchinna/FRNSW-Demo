using MAUI_Demo.Auth0;
using MAUI_Demo.MVVM.ViewModels;
using MAUI_Demo.MVVM.Views;
using MAUI_Demo.Views.Startup;
using MAUI_Demo_Service.Data;
using Microsoft.Extensions.Logging;

namespace MAUI_Demo;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        //Views
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<EmployeeView>();
		builder.Services.AddSingleton<EmployeeListViewModel>();

        builder.Services.AddSingleton<OktaSignIn>();
        builder.Services.AddSingleton(new Auth0Client(new()
        {
            Domain = "dev-17683470.okta.com",
            ClientId = "0oa912ox83mA6vxCh5d7",
            Scope = "openid profile offline_access",
            Audience = "https://dev-17683470.okta.com",
#if WINDOWS
                    RedirectUri = "https://localhost:44380/WeatherForecast"
#else
            RedirectUri = "myapp://callback"
#endif
        }));


        builder.Services.AddSingleton<TokenHandler>();


#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
