using HR.LeaveManagement.UI.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.UI.BlazorUI.Pages.LeaveTypes
{
	public partial class FormComponent
	{
		[Parameter] public bool Disabled { get; set; } = false;
		[Parameter] public LeaveTypeVM LeaveTypeVM { get; set; } = new();
		[Parameter] public string ButtonText { get; set; } = "Save";
		[Parameter] public EventCallback OnValidSubmit { get; set; }
	}
}
