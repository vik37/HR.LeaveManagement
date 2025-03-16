namespace HR.LeaveManager.Application.Models.Identities;

public class JwtSettings
{
	public string Key { get; set; } = string.Empty;
	public string Issue { get; set; } = string.Empty;
	public string Audience { get; set; } = string.Empty;
	public double DurationInMinutes { get; set; }
}
