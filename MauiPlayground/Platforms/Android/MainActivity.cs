using Android.App;
using Android.Content.PM;
using Android.OS;

namespace MauiPlayground;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
public class MainActivity : MauiAppCompatActivity
{
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    protected override void OnCreate(Bundle? savedInstanceState)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    {
		base.OnCreate(savedInstanceState);
		Platform.Init(this, savedInstanceState);
	}

	public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
	{
		Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

		base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
	}
}
