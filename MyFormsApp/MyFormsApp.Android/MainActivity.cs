using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Social.Services;
using MyFormsApp.Droid.Services;

namespace MyFormsApp.Droid
{
	[Activity(Label = "MyFormsApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			InitializeServices();

			LoadApplication(new App());
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			#region FacebookService
			var manager = DependencyService.Get<IFacebookService>();
			if (manager != null)
			{
				(manager as FacebookService).CallbackManager.OnActivityResult(requestCode, (int)resultCode, data);
			}
			#endregion
		}

		private void InitializeServices()
		{
			#region Facebook
			DependencyService.Register<IFacebookService, FacebookService>();
			#endregion
		}
	}
}

