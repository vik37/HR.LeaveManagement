using HR.LeaveManagement.UI.BlazorUI.Contracts;
using HR.LeaveManagement.UI.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.UI.BlazorUI.Pages.LeaveRequests
{
	public partial class Index
	{
		[Inject]
		ILeaveRequestService LeaveRequestService { get; set; }

		[Inject]
		NavigationManager NavigationManager { get; set; }

		public AdminLeaveRequestVM AdminLeaveRequestVM { get; set; } = new();

		protected override async Task OnInitializedAsync()
		{
			AdminLeaveRequestVM = await LeaveRequestService.GetAdminLeaveRequestList();
		}

		public void GoToDetails(int id)
		{
			NavigationManager.NavigateTo($"leaverequests/details/{id}");
		}
	}
}
