using HR.LeaveManagement.UI.BlazorUI.Models.LeaveTypes;

namespace HR.LeaveManagement.UI.BlazorUI.Models.LeaveAllocations
{
	public class LeaveAllocationVM
	{
		public int Id { get; set; }
		public int NumberOfDays { get; set; }
		public LeaveTypeVM LeaveType { get; set; } = new();
		public int LeaveTypeId { get; set; }
		public int Period { get; set; }
	}
}
