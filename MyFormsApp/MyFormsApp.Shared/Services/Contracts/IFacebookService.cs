using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Social.Data;

namespace Xamarin.Forms.Social.Services
{
	public interface IFacebookService
	{
		void Login(Action<FacebookUser, Exception> OnLoginCompleted);
		void Logout();
	}
}
