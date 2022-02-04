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

		builder.Services.AddSingleton<MainPageViewModel>();
		builder.Services.AddSingleton<NavigationService>();
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<Page2>();
		builder.Services.AddSingleton<Page2ViewModel>();

		return builder.Build();
		
	}

	public class MainPageViewModel
	{
		readonly NavigationService _navigationService;
		readonly IServiceProvider _services;
		public Command NavigateCommand => new Command(async () => await Navigate());
        public MainPageViewModel(NavigationService navService, IServiceProvider services)
        {
			_navigationService = navService;
			_services = services;
			
        }

		private Task Navigate()
			=> _navigationService.Navigation.PushAsync(_services.GetService<Page2>(), true);
    }

	public class Page2ViewModel
	{

		public Page2ViewModel(NavigationService navService)
		{

		}
	}

	public class NavigationService
    {
		public INavigation Navigation => App.Current.MainPage.Navigation;
    }
}
