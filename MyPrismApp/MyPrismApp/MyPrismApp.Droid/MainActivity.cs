using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Prism.Unity;
using Microsoft.Practices.Unity;
using MyPrismApp.Services.Contracts;
using MyPrismApp.Droid.Services;
using Xamarin.Forms;
using Xamarin.Facebook;
using Xamarin.Facebook.AppEvents;
using Android.Content;

namespace MyPrismApp.Droid
{
	[Activity(Label = "MyPrismApp", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		public static ICallbackManager CallbackManager { get; private set; }

		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.tabs;
			ToolbarResource = Resource.Layout.toolbar;

			base.OnCreate(bundle);
			FacebookSdk.ApplicationId = "1982655385305897";
			FacebookSdk.SdkInitialize(this);
			AppEventsLogger.ActivateApp(this);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			DependencyService.Register<IFacebookManager, FacebookManager>();

			if (CallbackManager == null)
			{
				CallbackManager = CallbackManagerFactory.Create();
			}

			LoadApplication(new App(new AndroidInitializer()));
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			if (CallbackManager != null)
				CallbackManager.OnActivityResult(requestCode, (int)resultCode, data);
			var manager = DependencyService.Get<IFacebookManager>();
			if (manager != null)
			{
				(manager as FacebookManager).CallbackManager.OnActivityResult(requestCode, (int)resultCode, data);
			}
		}
	}

	public class AndroidInitializer : IPlatformInitializer
	{
		public void RegisterTypes(IUnityContainer container)
		{
			container.RegisterInstance(DependencyService.Get<IFacebookManager>());
		}
	}
}

