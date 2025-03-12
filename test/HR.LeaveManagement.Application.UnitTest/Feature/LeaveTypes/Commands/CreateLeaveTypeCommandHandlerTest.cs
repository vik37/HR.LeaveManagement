using AutoMapper;
using HR.LeaveManagement.Application.UnitTest.Mocs;
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
		}

		[Fact]
		public async Task CreateLeaveTypeTest()
		{
			var handler = new CreateLeaveTypeCommandHandler(_mapper,_mockRepo.Object,_appLogger.Object);

			var command = new CreateLeaveTypeCommand() { Name = "Testing Leave Name", DefaultDays = 4 };
			var result = await handler.Handle(command, CancellationToken.None);

			var getLeaveDetail = new GetLeaveTypeDetailsQueryHandler(_mapper, _mockRepo.Object, _appLeaveDetailLogger.Object);

			var addedLeaveType = await getLeaveDetail.Handle(new GetLeaveTypeDetailsQuery(result), CancellationToken.None);
			result.ShouldBe(4);
			addedLeaveType.Name.ShouldBe(command.Name);
			addedLeaveType.Id.ShouldBe(result);
			addedLeaveType.DefaultDays.ShouldBe(command.DefaultDays);
		}
	}
}
