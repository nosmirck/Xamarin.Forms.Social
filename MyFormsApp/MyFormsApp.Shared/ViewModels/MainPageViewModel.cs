using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Social.Data;
using Xamarin.Forms.Social.Services;

namespace MyFormsApp.ViewModels
{
	class MainPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private string _message;
		private bool _isLoggedIn;
		private FacebookUser _facebookUser;
		private IFacebookService _facebookService;

		public string Message
		{
			get
			{
				return _message;
			}
			set
			{
				_message = value;
				OnPropertyChanged(nameof(Message));
			}
		}
		public bool IsLoggedIn
		{
			get
			{
				return _isLoggedIn;
			}
			set
			{
				_isLoggedIn = value;
				OnPropertyChanged(nameof(IsLoggedIn));
			}
		}
		public FacebookUser FacebookUser
		{
			get
			{
				return _facebookUser;
			}
			set
			{
				_facebookUser = value;
				OnPropertyChanged(nameof(FacebookUser));
			}
		}

		public Command FacebookLoginCommand { get; protected set; }
		public Command FacebookLogoutCommand { get; protected set; }

		public MainPageViewModel()
		{
			_facebookService = DependencyService.Get<IFacebookService>();

			Message = "Xamarin Forms Facebook Login";

			FacebookLoginCommand = new Command(FacebookLogin);
			FacebookLogoutCommand = new Command(FacebookLogout);
		}

		private void FacebookLogin()
		{
			_facebookService?.Login(OnLoginCompleted);
		}
		private void FacebookLogout()
		{
			_facebookService?.Logout();
			IsLoggedIn = false;
		}

		private void OnLoginCompleted(FacebookUser facebookUser, Exception exception)
		{
			if (exception == null)
			{
				FacebookUser = facebookUser;
				IsLoggedIn = true;
			}
			else
			{
				Debug.WriteLine("Error: " + exception.Message);
				App.Current.MainPage.DisplayAlert("Error", exception.Message, "OK");
			}
		}
	}
}
