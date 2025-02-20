namespace HR.LeaveManager.Application.Feature.LeaveType.Queries.GetLeaveTypeDetail;

public class LeaveTypeDetailDto
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public int DefaultDays { get; set; }
	public DateTime DateCreated { get; set; }
	public DateTime DateModified { get; set; }
}
