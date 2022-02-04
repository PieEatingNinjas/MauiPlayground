namespace MauiPlayground;

public class MainPageViewModel
{
    readonly NavigationService _navigationService;

    public string Text { get; set; } = string.Empty;

    public Command NavigateCommand => new Command(async () => await Navigate());
    public MainPageViewModel(NavigationService navService)
    {
        _navigationService = navService;

    }

    private Task Navigate()
        => _navigationService.NavigateToPage2(Text);
}
