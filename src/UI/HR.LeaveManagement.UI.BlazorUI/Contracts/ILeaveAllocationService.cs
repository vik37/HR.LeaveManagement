using HR.LeaveManagement.UI.BlazorUI.Services.Base;

namespace HR.LeaveManagement.UI.BlazorUI.Contracts;

public interface ILeaveAllocationService
{
	Task<Response<Guid>> CreateLeaveAllocations(int leaveTypeId);
}
