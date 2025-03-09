using HR.LeaveManager.Application.Feature.LeaveType.Queries.GetAllLeaveTypes;

namespace HR.LeaveManager.Application.Feature.LeaveRequest.Queries.GetLeaveRequestDetails;

public class LeaveRequestDetailsDto
{
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public string RequestingEmployeeId { get; set; } = string.Empty;
	public LeaveTypeDto LeaveTypeDto { get; set; } = new();
	public int LeaveTypeId { get; set; }
	public DateTime DateTimeRequested { get; set; }
	public string RequestComments { get; set; } = string.Empty;
	public DateTime? DateActioned { get; set; }
	public bool? Approved { get; set; }
	public bool Cancelled { get; set; }
}
