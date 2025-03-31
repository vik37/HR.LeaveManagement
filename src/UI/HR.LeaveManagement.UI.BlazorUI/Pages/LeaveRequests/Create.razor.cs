using Blazored.Toast.Services;
using HR.LeaveManagement.UI.BlazorUI.Contracts;
using HR.LeaveManagement.UI.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.UI.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.UI.BlazorUI.Pages.LeaveRequests
{
	public partial class Create
	{
		[Inject]
		ILeaveTypeService LeaveTypeService { get; set; }

		[Inject]
		ILeaveRequestService LeaveRequestService { get; set; }

		[Inject]
		NavigationManager NavigationManager { get; set; }

		[Inject]
		IToastService ToastService { get; set; }

		LeaveRequestVM LeaveRequest { get; set; } = new LeaveRequestVM();

		List<LeaveTypeVM> LeaveTypes { get; set; } = new List<LeaveTypeVM>();

		protected override async Task OnInitializedAsync()
		{
			LeaveTypes = await LeaveTypeService.GetLeaveTypes();
			LeaveRequest.StartDate = DateTime.Now;
			LeaveRequest.EndDate = DateTime.Now;
		}

		private async Task HandleValidSubmit()
		{
			await LeaveRequestService.CreateLeaveRequest(LeaveRequest);
			ToastService.ShowSuccess("Leave Request Created Successfully");
			NavigationManager.NavigateTo("/leaverequests/");
		}
	}
}
