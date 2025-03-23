using AutoMapper;
using Blazored.LocalStorage;
using HR.LeaveManagement.UI.BlazorUI.Contracts;
using HR.LeaveManagement.UI.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.UI.BlazorUI.Services.Base;

namespace HR.LeaveManagement.UI.BlazorUI.Services;

public class LeaveRequestService : BaseHttpService, ILeaveRequestService
{
	private readonly IMapper _mapper;
	public LeaveRequestService(IClient client, ILocalStorageService localStorageService, IMapper mapper) 
		: base(client, localStorageService)
	{
		_mapper = mapper;
	}

	public async Task<AdminLeaveRequestVM> GetAdminLeaveRequestList()
	{
		var leaveRequests = await _client.LeaveRequestAllAsync(isLoggedInUser: false);

		var model = new AdminLeaveRequestVM
		{
			TotalRequests = leaveRequests.Count,
			ApprovedRequests = leaveRequests.Count(q => q.Approval == true),
			PendingRequests = leaveRequests.Count(q => q.Approval == null),
			RejectedRequests = leaveRequests.Count(q => q.Approval == false),
			LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
		};

		return model;	
	}

	public Task<EmployeeLeaveRequestVM> GetEmployeeLeaveRequestList()
	{
		throw new NotImplementedException();
	}

	public Task<LeaveRequestVM> GetLeaveRequest(int id)
	{
		throw new NotImplementedException();
	}

	public async Task<Response<Guid>> CreateLeaveRequest(LeaveRequestVM request)
	{
		try
		{
			var response = new Response<Guid>();
			var createLeaveRequestCommand = _mapper.Map<CreateLeaveRequestCommand>(request);

			await _client.LeaveRequestPOSTAsync(createLeaveRequestCommand);
			return response;
		}
		catch (ApiException ex)
		{
			return ConvertApiExceptions(ex);
		}
	}

	public Task ApproveLeaveRequest(int id, bool approval)
	{
		throw new NotImplementedException();
	}

	public Task DeleteLeaveRequest(int id)
	{
		throw new NotImplementedException();
	}
}
