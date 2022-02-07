using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiPlayground;

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
            });

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<Page2>();
        builder.Services.AddSingleton<Page2ViewModel>();
        builder.Services.AddSingleton<Page3>();
        builder.Services.AddSingleton<NavigationService>();
        builder.Services.AddSingleton<IRepo, Repo>();

        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(Config.BaseUrl);

        builder.Services.AddSingleton(httpClient);

        return builder.Build();

    }
}