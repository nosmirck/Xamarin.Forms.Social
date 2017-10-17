using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPrismApp.Services.Data;

namespace MyPrismApp.Services.Contracts
{
	public interface IFacebookManager
	{
		void Login(Action<FacebookUser, Exception> OnLoginComplete);

		void Logout();
	}
}
