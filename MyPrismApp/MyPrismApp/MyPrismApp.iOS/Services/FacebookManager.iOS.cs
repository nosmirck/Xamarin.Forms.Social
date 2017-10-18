using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MyPrismApp.Services.Contracts;
using MyPrismApp.Services.Data;
using Facebook.LoginKit;
using Facebook.CoreKit;
using System.Diagnostics;

namespace MyPrismApp.iOS.Services
{
	public class FacebookManager : IFacebookManager
	{
		Action<FacebookUser, Exception> _onLoginComplete;

		LoginManager LoginManager;

		public FacebookManager()
		{
			LoginManager = new LoginManager();
		}

		void OnRequestHandler(GraphRequestConnection connection, NSObject result, NSError error)
		{
			if (error != null || result == null)
			{
				_onLoginComplete?.Invoke(null, new Exception(error.LocalizedDescription));
			}
			else
			{
				var id = string.Empty;
				var first_name = string.Empty;
				var email = string.Empty;
				var last_name = string.Empty;
				var url = string.Empty;

				try
				{
					id = result.ValueForKey(new NSString("id"))?.ToString();
				}
				catch (Exception e)
				{
					Debug.WriteLine(e.Message);
				}

				try
				{
					first_name = result.ValueForKey(new NSString("first_name"))?.ToString();
				}
				catch (Exception e)
				{
					Debug.WriteLine(e.Message);
				}

				try
				{
					email = result.ValueForKey(new NSString("email"))?.ToString();
				}
				catch (Exception e)
				{
					Debug.WriteLine(e.Message);
				}

				try
				{
					last_name = result.ValueForKey(new NSString("last_name"))?.ToString();
				}
				catch (Exception e)
				{
					Debug.WriteLine(e.Message);
				}

				try
				{
					url = ((result.ValueForKey(new NSString("picture")) as NSDictionary).ValueForKey(new NSString("data")) as NSDictionary).ValueForKey(new NSString("url")).ToString();
				}
				catch (Exception e)
				{
					Debug.WriteLine(e.Message);
				}
				_onLoginComplete?.Invoke(new FacebookUser(id, AccessToken.CurrentAccessToken.TokenString, first_name, last_name, email, url), null);
			}
		}
		void OnLoginHandler(LoginManagerLoginResult result, NSError error)
		{
			if (error != null || result == null || result.IsCancelled)
			{
				if (result != null && result.IsCancelled)
					_onLoginComplete?.Invoke(null, new Exception("Login Canceled."));

				if (error != null)
					_onLoginComplete?.Invoke(null, new Exception(error.LocalizedDescription));
			}
			else
			{
				var request = new GraphRequest("me", new NSDictionary("fields", "id, first_name, email, last_name, picture.width(500).height(500)"));
				request.Start(OnRequestHandler);
			}
		}
		#region IFacebookManager
		public void Login(Action<FacebookUser, Exception> OnLoginComplete)
		{
			if (_onLoginComplete == null)
				_onLoginComplete = OnLoginComplete;

			var window = UIApplication.SharedApplication.KeyWindow;
			var vc = window.RootViewController;
			while (vc.PresentedViewController != null)
			{
				vc = vc.PresentedViewController;
			}

			LoginManager.LogOut();
			LoginManager.LoginBehavior = LoginBehavior.Native;
			LoginManager.LogInWithReadPermissions(new string[] { "public_profile", "email" }, vc, OnLoginHandler);
		}

		public void Logout()
		{
			LoginManager.LogOut();
		}
		#endregion
	}
}