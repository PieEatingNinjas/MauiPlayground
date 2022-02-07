using System.Diagnostics;

namespace MauiPlayground;

public class NavigationService
{
    readonly IServiceProvider _services;

    internal INavigation Navigation
    {
        get
        {
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            INavigation? navigation = App.Current?.MainPage?.Navigation;
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

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
            //await page.ViewModel.PreInit(paramater);
            //Navigation.PushModalAsync()
            await Navigation.PushAsync(page, true);
            page.ViewModel.Init(parameter);
        }
    }

    public Task NavigateToPage3()
        => Navigation.PushAsync(_services.GetService<Page3>(), true);

    public Task NavigateBack()
        => Navigation.PopAsync(true);
}