using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManager.Application.Models.Identities;

public class RegisterRequest
{
	[Required]
	public string Firstname { get; set; } = string.Empty;
	[Required]
	public string Lastname { get; set; } = string.Empty;
	[Required]
	[EmailAddress]
	public string Email { get; set; } = string.Empty;
	[Required]
	[MinLength(6)]
	public string Username { get; set; } = string.Empty;
	[Required]
	[MinLength(6)]
	public string Password { get; set; } = string.Empty;
}
