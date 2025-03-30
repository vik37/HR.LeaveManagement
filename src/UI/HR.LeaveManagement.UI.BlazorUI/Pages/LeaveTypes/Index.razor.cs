using HR.LeaveManagement.UI.BlazorUI.Contracts;
using HR.LeaveManagement.UI.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.UI.BlazorUI.Pages.LeaveTypes
{
	public partial class Index
	{
		[Inject]
		public NavigationManager NavigationManager { get; set; }	

		[Inject]
		public ILeaveTypeService LeaveTypeService { get; set; }

		[Inject]
		private ILeaveAllocationService LeaveAllocationService { get; set; }

		public List<LeaveTypeVM>? LeaveTypes { get; private set; }
		public string Message { get; set; } = string.Empty;

		protected void CreateLeaveType()
		{
			NavigationManager.NavigateTo("/leavetypes/create/");
		}

		protected void AllocateLeaveType(int id)
		{
			LeaveAllocationService.CreateLeaveAllocations(id);
		}

		protected void DetailsLeaveType(int id)
		{
			NavigationManager.NavigateTo($"/leavetypes/details/{id}");
		}

		protected void UpdateLeaveType(int id)
		{
			NavigationManager.NavigateTo($"/leavetypes/edit/{id}");
		}

		protected async Task DeleteLeaveType(int id)
		{
			var response = await LeaveTypeService.DeleteLeaveType(id);
			if (response.Success)
			{
				LeaveTypes = LeaveTypes.Where(x => x.Id != id).ToList();
				StateHasChanged();
			}
			else
				Message = response.Message;
		}

		protected override async Task OnInitializedAsync()
		{
			LeaveTypes = await LeaveTypeService.GetLeaveTypes();
		}
	}
}