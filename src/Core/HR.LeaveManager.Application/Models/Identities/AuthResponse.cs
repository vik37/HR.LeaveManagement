namespace HR.LeaveManager.Application.Models.Identities;

public class AuthResponse
{
	public string Id { get; set; } = string.Empty;
	public string Username { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Token { get; set; } = string.Empty;
}
