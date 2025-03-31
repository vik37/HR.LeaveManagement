using HR.LeaveManagement.Identity.Models;
using HR.LeaveManager.Application.Contracts.Identity;
using HR.LeaveManager.Application.Exceptions;
using HR.LeaveManager.Application.Models.Identities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HR.LeaveManagement.Identity.Services;

public class UserService : IUserService
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly IHttpContextAccessor _contextAccessor;

	public UserService(UserManager<ApplicationUser> userManager,
		IHttpContextAccessor httpContextAccessor)
	{
		_userManager = userManager;
		_contextAccessor = httpContextAccessor;
	}

	public string? UserId { get => _contextAccessor.HttpContext is not null ?
			_contextAccessor.HttpContext?.User?.FindFirstValue("uid") : null; }

	public string? Role
	{
		get => _contextAccessor.HttpContext is not null ?
			_contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role) : null;
	}

	public async Task<List<Employee>> GetAllEmployees()
	{
		var employees = await _userManager.GetUsersInRoleAsync("Employee");
		return employees.Select(x => new Employee
		{
			Id = x.Id,
			Email = x.Email!,
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
