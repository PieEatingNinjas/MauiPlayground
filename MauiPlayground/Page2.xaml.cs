namespace MauiPlayground;

public partial class Page2 : ContentPage
{
	public Page2ViewModel ViewModel { get; }
	public Page2(Page2ViewModel vm)
	{
		BindingContext = ViewModel = vm;
		InitializeComponent();
	}
}

