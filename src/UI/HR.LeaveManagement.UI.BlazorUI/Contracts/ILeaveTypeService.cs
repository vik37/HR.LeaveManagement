using HR.LeaveManagement.UI.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.UI.BlazorUI.Services.Base;

namespace HR.LeaveManagement.UI.BlazorUI.Contracts;

public interface ILeaveTypeService
{
	Task<List<LeaveTypeVM>> GetLeaveTypes();
	Task<LeaveTypeVM> GetLeaveType(int id);
	Task<LeaveTypeDetailVM> GetLeaveTypeDetails(int id);
	Task<Response<Guid>> CreateLeaveType(LeaveTypeVM model);
	Task<Response<Guid>> UpdateLeaveType(LeaveTypeVM model);
	Task<Response<Guid>> DeleteLeaveType(int id);
}
