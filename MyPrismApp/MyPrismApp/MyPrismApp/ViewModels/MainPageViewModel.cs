using MyPrismApp.Services;
using MyPrismApp.Services.Contracts;
using MyPrismApp.Services.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPrismApp.ViewModels
{
	public class MainPageViewModel : BindableBase, INavigationAware
	{
		private string _title;
		private bool _isLoggedIn;
		private FacebookUser _facebookUser;
		IFacebookManager _facebookManager;
		IPageDialogService _pageDialogService;

		public DelegateCommand FacebookLoginCommand { get; set; }
		public DelegateCommand FacebookLogoutCommand { get; set; }

		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}
		public bool IsLoggedIn
		{
			get { return _isLoggedIn; }
			set { SetProperty(ref _isLoggedIn, value); }
		}
		public FacebookUser FacebookUser
		{
			get { return _facebookUser; }
			set { SetProperty(ref _facebookUser, value); }
		}

		public MainPageViewModel(IFacebookManager facebookManager, IPageDialogService pageDialogService)
		{
			_facebookManager = facebookManager;
			_pageDialogService = pageDialogService;
			FacebookLoginCommand = new DelegateCommand(FacebookLogin);
			FacebookLogoutCommand = new DelegateCommand(FacebookLogout);
		}
		private void OnLoginCompleted(FacebookUser facebookUser, Exception e)
		{
			if (e == null)
			{
				FacebookUser = facebookUser;
				IsLoggedIn = true;
			}
			else
			{
				_pageDialogService.DisplayAlertAsync("Error", e.Message, "Ok");
			}
		}
		private void FacebookLogin()
		{
			_facebookManager.Login(OnLoginCompleted);
		}

		private void FacebookLogout()
		{
			_facebookManager.Logout();
			IsLoggedIn = false;
		}

		public void OnNavigatedFrom(NavigationParameters parameters)
		{

		}

		public void OnNavigatingTo(NavigationParameters parameters)
		{

		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("title"))
				Title = (string)parameters["title"] + " and Prism";
			else
				Title = "Hello Facebook!";
		}
	}
}
