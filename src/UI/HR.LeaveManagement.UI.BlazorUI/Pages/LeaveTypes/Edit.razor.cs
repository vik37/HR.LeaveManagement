using HR.LeaveManagement.UI.BlazorUI.Contracts;
using HR.LeaveManagement.UI.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.UI.BlazorUI.Pages.LeaveTypes
{
	public partial class Edit
	{
		[Inject]
		NavigationManager NavigationManager { get; set; }

		[Inject]
		ILeaveTypeService LeaveTypeService { get; set; }

		[Parameter]
		public int Id { get; set; }

		public string Message { get; private set; } = string.Empty;

		public LeaveTypeVM LeaveTypeVM { get; set; } = new();

		protected override async Task OnParametersSetAsync()
		{
			LeaveTypeVM = await LeaveTypeService.GetLeaveType(Id);
		}

		async Task EditLeaveType()
		{
			var response = await LeaveTypeService.UpdateLeaveType(LeaveTypeVM);

			if (response.Success)
			{
				NavigationManager.NavigateTo("/leavetypes/");
				return;
			}
			Message = response.Message;
		}
	}
}
