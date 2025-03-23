using HR.LeaveManager.Application.Feature.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManager.Application.Models.Identities;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Queries.GetLeaveRequestList;

public class LeaveRequestListDto
{
	public int Id { get; set; }
	public Employee Employee { get; set; } = new();
	public string RequestingEmployeeId { get; set; } = string.Empty;
	public LeaveTypeDto LeaveTypeDto { get; set; } = new();
	public DateTime DateRequested { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public bool? Approval { get; set; }
}
