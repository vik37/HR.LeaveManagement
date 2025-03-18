using HR.LeaveManagement.UI.BlazorUI.Contracts;
using HR.LeaveManagement.UI.BlazorUI.Models;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.UI.BlazorUI.Pages
{
	public partial class Login
	{
		public LoginVM Model { get; set; }

		[Inject]
		public NavigationManager NavigationManager { get; set; }

		[Inject]
		private IAuthenticationService AuthenticationService { get; set; }

		public string Message { get; set; }

		public Login()
		{
			Model = new LoginVM();
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();
		}

		protected async Task HandleLogin()
		{
			if(await AuthenticationService.AuthenticateAsync(Model.Email, Model.Password))
				NavigationManager.NavigateTo("/");

			Message = "Invalid Username/Password";
		}
	}
}
