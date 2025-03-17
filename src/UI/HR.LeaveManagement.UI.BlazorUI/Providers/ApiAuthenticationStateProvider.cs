using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HR.LeaveManagement.UI.BlazorUI.Providers
{
	public class ApiAuthenticationStateProvider : AuthenticationStateProvider
	{
		private readonly ILocalStorageService _localStorageService;
		private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

		public ApiAuthenticationStateProvider(ILocalStorageService localStorageService)
		{
			_localStorageService = localStorageService;
			_jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var user = new ClaimsPrincipal(new ClaimsIdentity());
			var isTokenPresent = await _localStorageService.ContainKeyAsync("token");
			if (isTokenPresent)
				return new AuthenticationState(user);

			var savedToken = await _localStorageService.GetItemAsync<string>("token");
			var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);

			if(tokenContent.ValidTo < DateTime.Now)
			{
				await _localStorageService.RemoveItemAsync("token");
				return new AuthenticationState(user);
			}

			var claims = await GetClaims();

			user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
			return new AuthenticationState(user);
		}

		public async Task LoggedIn()
		{
			var claims = await GetClaims();
			var user = new ClaimsPrincipal(new ClaimsIdentity(claims,"jwt"));
			var authState = Task.FromResult(new AuthenticationState(user));
			NotifyAuthenticationStateChanged(authState);
		}

		public async Task LoggedOut()
		{
			await _localStorageService.RemoveItemAsync("token");
			var nobady = new ClaimsPrincipal(new ClaimsIdentity());
			var authState = Task.FromResult(new AuthenticationState(nobady));
			NotifyAuthenticationStateChanged(authState);
		}

		private async Task<List<Claim>> GetClaims()
		{
			var token = await _localStorageService.GetItemAsync<string>("token");
			var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(token);
			var claims = tokenContent.Claims.ToList();
			claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
			return claims;
		}
	}
}
