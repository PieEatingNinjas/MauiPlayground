using System.Diagnostics;

namespace MauiPlayground;

public class NavigationService
{
    readonly IServiceProvider _services;

    internal INavigation Navigation
    {
        get
        {
            INavigation? navigation = App.Current?.MainPage?.Navigation;

            if (navigation is not null)
                return navigation;
            else
            {
                //This is not good!
                if (Debugger.IsAttached)
                    Debugger.Break();
                throw new Exception();
            }
        }
    }

    public NavigationService(IServiceProvider services)
        => _services = services;    

    public async Task NavigateToPage2(string parameter)
    {
        var page = _services.GetService<Page2>();
        if (page != null)
        {
            await Navigation.PushAsync(page, true);
            page.ViewModel.Init(parameter);
        }
    }

    public Task NavigateToPage3()
        => Navigation.PushAsync(_services.GetService<Page3>(), true);

    public Task NavigateBack()
        => Navigation.PopAsync(true);
}