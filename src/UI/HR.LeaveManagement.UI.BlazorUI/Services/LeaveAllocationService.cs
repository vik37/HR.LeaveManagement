using Blazored.LocalStorage;
using HR.LeaveManagement.UI.BlazorUI.Contracts;
using HR.LeaveManagement.UI.BlazorUI.Services.Base;

namespace HR.LeaveManagement.UI.BlazorUI.Services;

public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
{
	public LeaveAllocationService(IClient client, ILocalStorageService localStorageService) 
		: base(client, localStorageService)
	{}

	public async Task<Response<Guid>> CreateLeaveAllocations(int leaveTypeId)
	{
		try
		{
			var response = new Response<Guid>();
			CreateLeaveAllocationCommand command = new() {LeaveTypeId = leaveTypeId};

			await _client.LeaveAllocationPOSTAsync(command);
			return response;
		}
		catch (ApiException ex)
		{
			return ConvertApiExceptions(ex);
		}
	}
}
