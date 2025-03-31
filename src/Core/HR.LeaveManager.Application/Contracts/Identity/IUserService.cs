using HR.LeaveManager.Application.Models.Identities;

namespace HR.LeaveManager.Application.Contracts.Identity;

public interface IUserService
{
	Task<List<Employee>> GetAllEmployees();
	Task<Employee> GetEmployeeById(string userId);

	public string? UserId { get; }
	public string? Role { get; }
}
