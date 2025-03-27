using HR.LeaveManagement.UI.BlazorUI.Contracts;
using HR.LeaveManagement.UI.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace HR.LeaveManagement.UI.BlazorUI.Pages.LeaveRequests
{
	public partial class EmployeeIndex
	{
		[Inject]
		ILeaveRequestService LeaveRequestService { get; set; }

		[Inject]
		NavigationManager NavigationManager { get; set; }

		[Inject]
		IJSRuntime js { get; set; }

		public EmployeeLeaveRequestVM EmployeeVM { get; set; } = new();

		public string Message { get; set; } = string.Empty;

		bool loaded = false;

		protected override async Task OnInitializedAsync()
		{
			EmployeeVM = await LeaveRequestService.GetEmployeeLeaveRequestList();
			loaded = true;
		}

		public async Task CancleLeaveRequest(int id)
		{
			var confirm = await js.InvokeAsync<bool>("confirm", "Are you sure to cancle this request?");
			var response = await LeaveRequestService.CancleLeaveRequest(id);

			if(confirm)
			{
				if (response.Success)
				{
					StateHasChanged();
					return;
				}

				Message = response.Message;
			}
		}
	}
}
