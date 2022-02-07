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

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

    public void Init(string parameter)
    {
        Title = parameter;
    }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    private void RiasePropertyChanged([CallerMemberName]string? property = null)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

    public Command NavigateBackCommand => new Command(async () => await _navigationService.NavigateBack());
    public Command NavigateCommand => new Command(async () => await _navigationService.NavigateToPage3());
}
