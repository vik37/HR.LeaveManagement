using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.UI.BlazorUI.Models.LeaveTypes;

public class LeaveTypeVM
{
	public int Id { get; set; }

	[Required(ErrorMessage = "Name is required")]
	public string Name { get; set; } = string.Empty;

	[Required(ErrorMessage = "Default Days are required")]
	[Display(Name = "Default Number of Days")]
	public int DefaultDays { get; set; }
}
