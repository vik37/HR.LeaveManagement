﻿using HR.LeaveManagement.UI.BlazorUI.Models;
using HR.LeaveManagement.UI.BlazorUI.Services.Base;

namespace HR.LeaveManagement.UI.BlazorUI.Contracts;

public interface ILeaveTypeService
{
	Task<List<LeaveTypeVM>> GetLeaveTypes();
	Task<LeaveTypeVM> GetLeaveTypeDetails(int id);
	Task<Response<Guid>> CreateLeaveType(LeaveTypeVM model);
	Task<Response<Guid>> UpdateLeaveType(LeaveTypeVM model);
	Task<Response<Guid>> DeleteLeaveType(int id);
}
