using HR.LeaveManagement.UI.BlazorUI.Contracts;
using HR.LeaveManagement.UI.BlazorUI.Services.Base;

namespace HR.LeaveManagement.UI.BlazorUI.Services;

public class LeaveTypeService : BaseHttpService, ILeaveTypeService
{
	public LeaveTypeService(IClient client) : base(client)
	{
	}
}
