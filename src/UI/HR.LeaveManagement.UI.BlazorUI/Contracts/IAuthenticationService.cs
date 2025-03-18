using HR.LeaveManagement.UI.BlazorUI.Models;

namespace HR.LeaveManagement.UI.BlazorUI.Contracts
{
	public interface IAuthenticationService
	{
		Task<bool> AuthenticateAsync(string email, string password);
		Task<bool> RegisterAsync(RegisterVM register);
		Task Logout();
	}
}
