using MAUI_Demo.Auth0;
using MAUI_Demo.ViewModels;
using MAUI_Demo.Views.Startup;
using MAUI_Demo_Service.Data;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        builder.Services.AddSingleton<TokenHandler>();


#if DEBUG
        builder.Logging.AddDebug();
#endif

        Debug.WriteLine("My APP: " + "Application started customlog");
        AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
        {
            //bool answer = DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
            Debug.WriteLine("Answer: " + error.ExceptionObject.ToString());
            //MessageBox.Show(text: error.ExceptionObject.ToString(), caption: "Error");
        };


        return builder.Build();
	}
}
