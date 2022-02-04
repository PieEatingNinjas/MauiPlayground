using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiPlayground;

public class Page2ViewModel : INotifyPropertyChanged
{
    readonly NavigationService _navigationService;

    private string _title = string.Empty;

    public string Title
    {
        get { return _title; }
        set { _title = value; RiasePropertyChanged();  }
    }


    public Page2ViewModel(NavigationService navService)
    {
        _navigationService = navService;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void Init(string parameter)
    {
        Title = parameter;
    }

    private void RiasePropertyChanged([CallerMemberName]string? property = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

    public Command NavigateBackCommand => new Command(async () => await _navigationService.NavigateBack());
    public Command NavigateCommand => new Command(async () => await _navigationService.NavigateToPage3());
}
