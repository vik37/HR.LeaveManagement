﻿using HR.LeaveManager.Application.Feature.LeaveType.Queries.GetAllLeaveTypes;

namespace HR.LeaveManager.Application.Feature.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class LeaveAllocationDetailsDto
{
	public int Id { get; set; }
	public int NumberOfDays { get; set; }
	public LeaveTypeDto LeaveType { get; set; }
	public int LeaveTypeId { get; set; }
	public int Period { get; set; }
}
