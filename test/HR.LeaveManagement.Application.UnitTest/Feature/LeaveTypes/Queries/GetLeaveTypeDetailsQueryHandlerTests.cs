using AutoMapper;
using HR.LeaveManagement.Application.UnitTest.Mocs;
using HR.LeaveManagement.Domain;
using HR.LeaveManager.Application.Contracts.Identity;
using HR.LeaveManager.Application.Contracts.Logging;
using HR.LeaveManager.Application.Contracts.Persistence;
using HR.LeaveManager.Application.Exceptions;
using HR.LeaveManager.Application.Feature.LeaveType.Queries.GetLeaveTypeDetail;
using HR.LeaveManager.Application.MappingProfiles;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Application.UnitTest.Feature.LeaveTypes.Queries;

public class GetLeaveTypeDetailsQueryHandlerTests
{
	private readonly Mock<ILeaveTypeRepository> _mockRepo;
	private readonly IMapper _mapper;
	private readonly Mock<IAppLogger<GetLeaveTypeDetailsQueryHandler>> _appLogger;
	private readonly Mock<IUserService> _userService;

	public GetLeaveTypeDetailsQueryHandlerTests()
	{
		_mockRepo = MockLeaveTypeRepository.GetLeaveTypesMockLeaveTypeRepository();

		var mappConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<LeaveTypeProfile>();
		});

		_mapper = mappConfig.CreateMapper();
		_appLogger = new Mock<IAppLogger<GetLeaveTypeDetailsQueryHandler>>();
		_userService = new Mock<IUserService>();
	}

	[Fact]
	public async Task GetLeaveTypeDetailsTest()
	{
		int id = 2;
		var handler = new GetLeaveTypeDetailsQueryHandler(_mapper, _mockRepo.Object, _userService.Object, _appLogger.Object);

		var result = await handler.Handle(new GetLeaveTypeDetailsQuery(id), CancellationToken.None);

		result.DefaultDays.ShouldBe(15);
		result.Name.ShouldEndWith("Sick");
		result.Name.ShouldNotBeNullOrEmpty();
		result.Id.ShouldBe(id);
	}

	[Fact]
	public async Task GetLeaveTypeDetailsTest_ShouldThrowNotFoundException()
	{
		int id = 12;
		var handler = new GetLeaveTypeDetailsQueryHandler(_mapper, _mockRepo.Object, _userService.Object, _appLogger.Object);

		var exceptionExpect = await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(new GetLeaveTypeDetailsQuery(id), CancellationToken.None));
		exceptionExpect.Message.ShouldNotBeNullOrEmpty();
		exceptionExpect.Message.ShouldBe($"{nameof(LeaveType)} ({id}) was not found");
	}

	[Fact]
	public async Task Handle_NonAdministratorRole_ShouldHandleNullCreatedByOrModifiedBy()
	{
		// Arrange
		int id = 4;
		var mockedSetupUserService = MockUserService.GetMockedUserLoggedCredentials("Employee");

		var handler = new GetLeaveTypeDetailsQueryHandler(_mapper, _mockRepo.Object, mockedSetupUserService.Object, _appLogger.Object);

		// Act
		var result = await handler.Handle(new GetLeaveTypeDetailsQuery(id), CancellationToken.None);

		// Assert
		result.CreatedBy.ShouldBeNull(); // Should remain null
		result.ModifiedBy.ShouldBeNull();
		result.Name.ShouldBe("Sick Leave");
	}

	[Fact]
	public async Task Handle_AdministratorRole_ShouldHandleModifiedBy_ShouldContainsUserEmailLastTimeModifiedLeaveType()
	{
		// Arrange
		int id = 4;
		var mockedSetupUserService = MockUserService.GetMockedUserLoggedCredentials();
		var handler = new GetLeaveTypeDetailsQueryHandler(_mapper, _mockRepo.Object, mockedSetupUserService.Object, _appLogger.Object);

		// Act
		var result = await handler.Handle(new GetLeaveTypeDetailsQuery(id), CancellationToken.None);

		// Assert
		result.CreatedBy.ShouldNotBeNull(); // Should remain null
		result.ModifiedBy.ShouldNotBeNull();
		result.Name.ShouldBe("Sick Leave");
		result.CreatedBy.ShouldBe(MockUserService.Email);
		result.ModifiedBy.ShouldBe(MockUserService.Email);
	}
}
