using HR.LeaveManagement.UI.BlazorUI.Contracts;
using HR.LeaveManagement.UI.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.UI.BlazorUI.Pages.LeaveTypes
{
	public partial class Create
	{
		[Inject]
		NavigationManager NavigationManager { get; set; }

		[Inject]
		ILeaveTypeService LeaveTypeService { get; set; }
		public string Message { get; private set; }

		public LeaveTypeVM LeaveTypeVM { get; set; } = new LeaveTypeVM();

		async Task CreateLeaveType()
		{
			var response = await LeaveTypeService.CreateLeaveType(LeaveTypeVM);

			if (response.Success)
				NavigationManager.NavigateTo("/leavetypes/");

			Message = response.Message;
		}
	}
}
