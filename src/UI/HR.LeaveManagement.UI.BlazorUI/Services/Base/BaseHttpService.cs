using Blazored.LocalStorage;
using HR.LeaveManagement.UI.BlazorUI.Services.Base;
using System.Net;
using System.Net.Http.Headers;

public class BaseHttpService
{
	protected  IClient _client;
	protected readonly ILocalStorageService _localStorageService;

	public BaseHttpService(IClient client, ILocalStorageService localStorageService)
	{
		_client = client;
		_localStorageService = localStorageService;
	}

	protected Response<Guid> ConvertApiExceptions(ApiException ex)
	{
		switch (ex.StatusCode)
		{
			case (int)HttpStatusCode.BadRequest:
				return new Response<Guid> { Message = "Invalid data was submitted",Success = false };
			case (int)HttpStatusCode.NotFound:
				return new Response<Guid> { Message = "The record was not found", Success = false };
			default:
				return new Response<Guid> { Message = "Something went wrong, please try again later", Success = false };
		}
	}

	protected async Task AddBearerToken()
	{
		if (await _localStorageService.ContainKeyAsync("token"))
			_client.HttpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Bearer", await _localStorageService.GetItemAsync<string>("token"));
	}
}
