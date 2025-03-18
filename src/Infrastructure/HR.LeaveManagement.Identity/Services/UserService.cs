using HR.LeaveManagement.Identity.Models;
using HR.LeaveManager.Application.Contracts.Identity;
using HR.LeaveManager.Application.Exceptions;
using HR.LeaveManager.Application.Models.Identities;
using Microsoft.AspNetCore.Identity;

namespace HR.LeaveManagement.Identity.Services;

public class UserService : IUserService
{
	private readonly UserManager<ApplicationUser> _userManager;

	public UserService(UserManager<ApplicationUser> userManager)
	{
		_userManager = userManager;
	}

	public async Task<List<Employee>> GetAllEmployees()
	{
		var employees = await _userManager.GetUsersInRoleAsync("Employee");
		return employees.Select(x => new Employee
		{
			Id = x.Id,
			Email = x.Email,
			Firstname = x.Firstname, 
			Lastname = x.Lastname
		}).ToList();
	}

	public async Task<Employee> GetEmployeeById(string userId)
	{
		var employee = await _userManager.FindByIdAsync(userId);

		if (employee is null)
			throw new NotFoundException("User does not exist", 0);

		return new Employee
		{
			Id = userId,
			Email = employee.Email,
			Firstname = employee.Firstname,
			Lastname = employee.Lastname
		};
	}
}
