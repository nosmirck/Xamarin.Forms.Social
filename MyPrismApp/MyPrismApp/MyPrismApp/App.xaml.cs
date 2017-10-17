using Prism.Unity;
using MyPrismApp.Views;
using Xamarin.Forms;
using MyPrismApp.ViewModels;

namespace MyPrismApp
{
	public partial class App : PrismApplication
	{
		public App(IPlatformInitializer initializer = null) : base(initializer) { }

		protected override void OnInitialized()
		{
			InitializeComponent();

			NavigationService.NavigateAsync("NavigationPage/MainPage");
		}

		protected override void RegisterTypes()
		{
			Container.RegisterTypeForNavigation<NavigationPage>();
			Container.RegisterTypeForNavigation<MainPage, MainPageViewModel>();
		}
	}
}
