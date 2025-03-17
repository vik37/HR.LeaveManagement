using Blazored.LocalStorage;
using HR.LeaveManagement.UI.BlazorUI.Contracts;
using HR.LeaveManagement.UI.BlazorUI.Providers;
using HR.LeaveManagement.UI.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagement.UI.BlazorUI.Services
{
	public class AuthenticationService : BaseHttpService, IAuthenticationService
	{
		private readonly AuthenticationStateProvider _authStateProvider;
		public AuthenticationService(IClient client, ILocalStorageService localStorageService,
			AuthenticationStateProvider authStateProvider) 
			: base(client, localStorageService)
		{
			_authStateProvider = authStateProvider;
		}

		public async Task<bool> AuthenticateAsync(string email, string password)
		{
			try
			{
				var authRequest = new AuthRequest { Email = email, Password = password };
				var authResponse = await _client.LoginAsync(authRequest);
				if (!string.IsNullOrEmpty(authResponse.Token))
				{
					await _localStorageService.SetItemAsync("token", authResponse.Token);

					// Set claims in Blazor and login state.
					await ((ApiAuthenticationStateProvider) _authStateProvider).LoggedIn();
					return true;
				}

				return false;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<bool> Registerasync(string firstname, string lastname, string username, string email, string password)
		{
			var registerRequest = new RegisterRequest { Firstname = firstname, Lastname = lastname, 
				Username = username, Email = email,	Password = password };

			var response = await _client.RegisterAsync(registerRequest);

			if(!string.IsNullOrEmpty(response.UserId))
				return true;
			return false;
		}

		public async Task Logout()
		{
			// Remove claims in Blazor and login state.
			await ((ApiAuthenticationStateProvider)_authStateProvider).LoggedOut();
		}		
	}
}
