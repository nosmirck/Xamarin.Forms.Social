using MyPrismApp.Services.Contracts;
using Xamarin.Forms;

namespace MyPrismApp.Services
{
	public static class FacebookManager
	{
		public static IFacebookManager GetManager()
		{
			return DependencyService.Get<IFacebookManager>();
		}
	}
}
