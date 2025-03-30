using HR.LeaveManagement.UI.BlazorUI.Contracts;
using HR.LeaveManagement.UI.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.UI.BlazorUI.Pages.LeaveTypes
{
	public partial class Details
	{
		[Inject]
		NavigationManager NavigationManager { get; set; }

		[Inject]
		ILeaveTypeService LeaveTypeService { get; set; }

		[Parameter]
		public int Id { get; set; }

		public LeaveTypeDetailVM LeaveTypeDetailVM { get; set; } = new();

		protected override async Task OnParametersSetAsync()
		{
			LeaveTypeDetailVM = await LeaveTypeService.GetLeaveTypeDetails(Id);
		}

		protected void UpdateLeaveType(int id)
		{
			NavigationManager.NavigateTo($"/leavetypes/edit/{id}");
		}
	}
}
