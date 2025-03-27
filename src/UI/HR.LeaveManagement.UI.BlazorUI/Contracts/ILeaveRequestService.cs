using HR.LeaveManagement.UI.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.UI.BlazorUI.Services.Base;

namespace HR.LeaveManagement.UI.BlazorUI.Contracts;

public interface ILeaveRequestService
{
	Task<AdminLeaveRequestVM> GetAdminLeaveRequestList();
	Task<EmployeeLeaveRequestVM> GetEmployeeLeaveRequestList();
	Task<LeaveRequestVM> GetLeaveRequest(int id);	
	Task DeleteLeaveRequest(int id);
	Task<Response<Guid>> CreateLeaveRequest(LeaveRequestVM request);
	Task<Response<Guid>> ApproveLeaveRequest(int id, bool approval);
	Task<Response<Guid>> CancleLeaveRequest(int id);
}
