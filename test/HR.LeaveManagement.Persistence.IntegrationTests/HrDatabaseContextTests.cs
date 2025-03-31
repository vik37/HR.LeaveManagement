using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManager.Application.Contracts.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests;

public class HrDatabaseContextTests
{
	private readonly HRDatabaseContext _context;
	private readonly Mock<IUserService> _userService;

	public HrDatabaseContextTests()
	{
		var dbOptions = new DbContextOptionsBuilder<HRDatabaseContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

		_userService = new Mock<IUserService>();

		_context = new HRDatabaseContext(dbOptions, _userService.Object);
	}

	[Fact]
	public async void Save_SetDateCreatedValue()
	{
		// Arange
		var leaveType = new LeaveType
		{
			Id = 1,
			Name = "Test Vacation",
			DefaultDays = 10
		};

		// Action
		await _context.LeaveTypes.AddAsync(leaveType);
		await _context.SaveChangesAsync();

		var savedLeaveType = _context.LeaveTypes.FirstOrDefault();

		// Assert
		Assert.True(leaveType.DateCreated > DateTime.MinValue);
		savedLeaveType.ShouldBeEquivalentTo(leaveType);
		savedLeaveType?.Id.ShouldBe(leaveType.Id);
		savedLeaveType.ShouldNotBeNull();
	}

	[Fact]
	public async void Save_SetDateModifiedValue()
	{
		// Arange
		var leaveType = new LeaveType
		{
			Id = 1,
			Name = "Test Vacation",
			DefaultDays = 10
		};

		// Action
		await _context.LeaveTypes.AddAsync(leaveType);
		await _context.SaveChangesAsync();

		// Assert
		leaveType.DateModified.ShouldNotBeNull();
	}

	[Fact]
	public async void Delete_LeaveTypeEntityinDb_ShouldBeEmpty()
	{
		var leaveType = new LeaveType
		{
			Id = 1,
			Name = "Test Vacation",
			DefaultDays = 10
		};

		await _context.LeaveTypes.AddAsync(leaveType);
		await _context.SaveChangesAsync();

		// Test Saved - LeaveType
		var savedLeaveType = _context.LeaveTypes.FirstOrDefault(x => x.Id == leaveType.Id);

		savedLeaveType.ShouldNotBeNull();

		// Test Removed - LeaveType
		_context.LeaveTypes.Remove(leaveType);
		await _context.SaveChangesAsync();

		var afterRemoveLeaveType = _context.LeaveTypes.FirstOrDefault(x => x.Id == leaveType.Id);
		afterRemoveLeaveType.ShouldBeNull();
	}
}
