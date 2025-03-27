using HR.LeaveManagement.UI.BlazorUI.Contracts;
using HR.LeaveManagement.UI.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.UI.BlazorUI.Pages.LeaveRequests
{
	public partial class Details
	{
		[Inject]
		ILeaveRequestService LeaveRequestService { get; set; }

		[Inject]
		NavigationManager NavigationManager { get; set; }

		[Parameter]
		public int Id { get; set; }

		string ClassName;
		string HeadingText;
		bool loaded = false;

		public LeaveRequestVM RequestModel { get; private set; } = new();

		protected override async Task OnParametersSetAsync()
		{
			RequestModel = await LeaveRequestService.GetLeaveRequest(Id);
			loaded = true;
		}

		protected override async Task OnInitializedAsync()
		{
			await Task.Run(() =>
			{
				if (RequestModel is not null)
				{
					if (RequestModel.Approved is null)
					{
						ClassName = "warning";
						HeadingText = "Pending Approval";
					}
					else if (RequestModel.Approved == true)
					{
						ClassName = "success";
						HeadingText = "";
					}
				}
			});
		}

		async Task ChangeApproval(bool approvalStatus)
		{
			await LeaveRequestService.ApproveLeaveRequest(Id, approvalStatus);
			NavigationManager.NavigateTo("/leaverequests/");
		}
	}
}
