using AutoMapper;
using HR.LeaveManagement.Application.UnitTest.Mocs;
using HR.LeaveManagement.Domain;
using HR.LeaveManager.Application.Contracts.Identity;
using HR.LeaveManager.Application.Contracts.Logging;
using HR.LeaveManager.Application.Contracts.Persistence;
using HR.LeaveManager.Application.Feature.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManager.Application.Feature.LeaveType.Queries.GetLeaveTypeDetail;
using HR.LeaveManager.Application.MappingProfiles;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Application.UnitTest.Feature.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTest
    {
		private readonly Mock<ILeaveTypeRepository> _mockRepo;
		private readonly IMapper _mapper;
		private readonly Mock<IAppLogger<CreateLeaveTypeCommandHandler>> _appLogger;
		private readonly Mock<IAppLogger<GetLeaveTypeDetailsQueryHandler>> _appLeaveDetailLogger;
		private readonly Mock<IUserService> _userService;

		public CreateLeaveTypeCommandHandlerTest()
		{
			_mockRepo = MockLeaveTypeRepository.GetLeaveTypesMockLeaveTypeRepository();

			var mappConfig = new MapperConfiguration(c =>
			{
				c.AddProfile<LeaveTypeProfile>();
			});

			_mapper = mappConfig.CreateMapper();
			_appLogger = new Mock<IAppLogger<CreateLeaveTypeCommandHandler>>();
			_appLeaveDetailLogger = new Mock<IAppLogger<GetLeaveTypeDetailsQueryHandler>>();
			_userService = new Mock<IUserService>();
		}

		[Fact]
		public async Task CreateLeaveTypeTest()
		{
			var handler = new CreateLeaveTypeCommandHandler(_mapper,_mockRepo.Object,_appLogger.Object);

			var command = new CreateLeaveTypeCommand() { Name = "Testing Leave Name", DefaultDays = 4 };
			var result = await handler.Handle(command, CancellationToken.None);

			var getLeaveDetail = new GetLeaveTypeDetailsQueryHandler(_mapper, _mockRepo.Object, _userService.Object, _appLeaveDetailLogger.Object);

			var addedLeaveType = await getLeaveDetail.Handle(new GetLeaveTypeDetailsQuery(result), CancellationToken.None);
			result.ShouldBe(5);
			addedLeaveType.Name.ShouldBe(command.Name);
			addedLeaveType.Id.ShouldBe(result);
			addedLeaveType.DefaultDays.ShouldBe(command.DefaultDays);
		}

		[Fact]
		public async Task CreateLeaveType_ShouldContainsCreatedByUserId_TestTheProperUserEmailWhichCreatedNewLeaveType()
		{
			var setupMockUserService = MockUserService.GetMockedUserLoggedCredentials("Administrator");
			var handler = new CreateLeaveTypeCommandHandler(_mapper, _mockRepo.Object, _appLogger.Object);

			var command = new CreateLeaveTypeCommand() { Name = "Testing Leave Name", DefaultDays = 4 };
			var result = await handler.Handle(command, CancellationToken.None);

			var getLeaveDetail = new GetLeaveTypeDetailsQueryHandler(_mapper, _mockRepo.Object, setupMockUserService.Object, _appLeaveDetailLogger.Object);

			var addedLeaveType = await getLeaveDetail.Handle(new GetLeaveTypeDetailsQuery(result), CancellationToken.None);
			
			addedLeaveType.Name.ShouldBe(command.Name);
			addedLeaveType.Id.ShouldBe(result);
			addedLeaveType.DateCreated.ShouldNotBe(default);
			addedLeaveType.DateCreated.ShouldBeInRange(DateTime.Now.AddMilliseconds(-100), DateTime.Now.AddSeconds(2));
			addedLeaveType.CreatedBy.ShouldBe(MockUserService.Email);
		}
	}
}
