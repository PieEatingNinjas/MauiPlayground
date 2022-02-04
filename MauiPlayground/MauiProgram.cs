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

        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<NavigationService>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<Page2>();
        builder.Services.AddSingleton<Page2ViewModel>();
        builder.Services.AddSingleton<Page3>();

        return builder.Build();

    }
}

public class MainPageViewModel
{
    readonly NavigationService _navigationService;

    public string Text { get; set; }

    public Command NavigateCommand => new Command(async () => await Navigate());
    public MainPageViewModel(NavigationService navService)
    {
        _navigationService = navService;

    }

    private Task Navigate()
        => _navigationService.NavigateToPage2(Text);
}

public class Page2ViewModel : INotifyPropertyChanged
{
    readonly NavigationService _navigationService;

    private string _title;

    public string Title
    {
        get { return _title; }
        set { _title = value; RiasePropertyChanged();  }
    }


    public Page2ViewModel(NavigationService navService)
    {
        _navigationService = navService;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void Init(string parameter)
    {
        Title = parameter;
    }

    private void RiasePropertyChanged([CallerMemberName]string property = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

    public Command NavigateBackCommand => new Command(async () => await _navigationService.NavigateBack());
    public Command NavigateCommand => new Command(async () => await _navigationService.NavigateToPage3());

}

public class NavigationService
{
    public INavigation Navigation => App.Current.MainPage.Navigation;
    readonly IServiceProvider _services;

    public NavigationService(IServiceProvider services)
    {
        _services = services;
    }

    public async Task NavigateToPage2(string parameter)
    {
        var p = _services.GetService<Page2>();
        await Navigation.PushAsync(p, true);
        p.ViewModel.Init(parameter);
    }
    public Task NavigateToPage3()
        => Navigation.PushAsync(_services.GetService<Page3>(), true);
    
    public Task NavigateBack()
        => Navigation.PopAsync();
}