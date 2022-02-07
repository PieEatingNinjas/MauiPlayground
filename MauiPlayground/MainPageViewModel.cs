namespace MauiPlayground;

public class MainPageViewModel
{
    readonly NavigationService _navigationService;
    readonly IRepo _repository;

    public string Text { get; set; } = string.Empty;

    public Command NavigateCommand => new Command(async () => await Navigate());

    public MainPageViewModel(NavigationService navService, IRepo repo)
    {
        _navigationService = navService;
        _repository = repo;

    }

    private Task Navigate()
    {
        _repository.Test();
        return _navigationService.NavigateToPage2(Text);
    }
}
