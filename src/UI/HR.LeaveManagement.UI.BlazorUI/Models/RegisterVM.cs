using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.UI.BlazorUI.Models
{
	public class RegisterVM
	{
		[Required]
		public string Firstname { get; set; }

		[Required]
		public string Lastname { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MinLength(6)]
		public string Password { get; set; }
	}
}
