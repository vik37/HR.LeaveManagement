namespace HR.LeaveManagement.UI.BlazorUI.Contracts
{
	public interface IAuthenticationService
	{
		Task<bool> AuthenticateAsync(string email, string password);
		Task<bool> Registerasync(string firstname, string lastname, string username, string email, string password);
		Task Logout();
	}
}
