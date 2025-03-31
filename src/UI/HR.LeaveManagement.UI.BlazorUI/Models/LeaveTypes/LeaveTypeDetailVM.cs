namespace HR.LeaveManagement.UI.BlazorUI.Models.LeaveTypes
{
	public class LeaveTypeDetailVM
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public int DefaultDays { get; set; }
		public DateTime DateCreated { get; set; }
		public string CreatedBy { get; set; }		
		public DateTime DateModified { get; set; }
		public string ModifiedBy { get; set; }
	}
}
