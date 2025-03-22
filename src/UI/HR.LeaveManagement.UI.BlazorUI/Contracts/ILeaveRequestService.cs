using HR.LeaveManagement.UI.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.UI.BlazorUI.Services.Base;

namespace HR.LeaveManagement.UI.BlazorUI.Contracts;

public interface ILeaveRequestService
{
	Task<AdminLeaveRequestVM> GetAdminLeaveRequestList();
	Task<EmployeeLeaveRequestVM> GetEmployeeLeaveRequestList();
	Task<Response<Guid>> CreateLeaveRequest(LeaveRequestVM request);
	Task<LeaveRequestVM> GetLeaveRequest(int id);
	Task DeleteLeaveRequest(int id);
	Task ApproveLeaveRequest(int id, bool approval);
}
