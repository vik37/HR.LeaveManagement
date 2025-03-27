using HR.LeaveManagement.UI.BlazorUI.Models.LeaveAllocations;

namespace HR.LeaveManagement.UI.BlazorUI.Models.LeaveRequests
{
	public class EmployeeLeaveRequestVM
	{
		public List<LeaveAllocationVM> LeaveAllocationVMs { get; set; }
		public List<LeaveRequestVM> LeaveRequestVMs { get; set; }
	}
}
