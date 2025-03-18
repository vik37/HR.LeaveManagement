using HR.LeaveManagement.UI.BlazorUI.Contracts;
using HR.LeaveManagement.UI.BlazorUI.Models;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.UI.BlazorUI.Pages
{
	public partial class Register
	{
		public RegisterVM Model { get; set; }

		[Inject]
		public NavigationManager NavigationManager { get; set; }

		[Inject]
		private IAuthenticationService AuthenticationService { get; set; }

		public string Message { get; set; }

		protected override void OnInitialized()
		{
			Model = new RegisterVM();
		}

		protected async Task HandleRegister()
		{
			var result = await AuthenticationService.RegisterAsync(Model);

			if (result)
				NavigationManager.NavigateTo("/");

			Message = "Something went wrong, please try again!";
		}
	}
}
