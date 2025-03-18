using HR.LeaveManagement.UI.BlazorUI.Contracts;
using HR.LeaveManagement.UI.BlazorUI.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagement.UI.BlazorUI.Pages
{
	public partial class Home
	{
		[Inject]
		public NavigationManager NavigationManager { get; set; }
		[Inject] 
		IAuthenticationService AuthenticationService { get; set; }
		[Inject]
		AuthenticationStateProvider AuthenticationStateProvider { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await ((ApiAuthenticationStateProvider) AuthenticationStateProvider).GetAuthenticationStateAsync();
		}

		protected void GotToLogin()
		{
			NavigationManager.NavigateTo("login/");
		}

		protected void GotToRegister()
		{
			NavigationManager.NavigateTo("register/");
		}

		protected async Task Logout()
		{
			await AuthenticationService.Logout();
		}
	}
}
