namespace MauiPlayground;

public partial class Page2 : ContentPage
{
	public Page2(MauiProgram.Page2ViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}

