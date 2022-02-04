namespace MauiPlayground;

public partial class MainPage : ContentPage
{
	

	public MainPage(MauiProgram.MainPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}

