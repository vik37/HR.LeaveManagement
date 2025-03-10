using AutoMapper;
using Castle.Core.Logging;
using HR.LeaveManagement.Application.UnitTest.Mocs;
using HR.LeaveManager.Application.Contracts.Logging;
using HR.LeaveManager.Application.Contracts.Persistence;
using HR.LeaveManager.Application.Feature.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManager.Application.Feature.LeaveType.Queries.GetLeaveTypeDetail;
using HR.LeaveManager.Application.MappingProfiles;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Application.UnitTest.Feature.LeaveTypes.Queries;

public class GetLeaveTypeListsQueryHandlerTests
{
	private readonly Mock<ILeaveTypeRepository> _mockRepo;
	private readonly IMapper _mapper;
	private readonly Mock<IAppLogger<GetLeaveTypesQueryHandler>> _appLogger;

	public GetLeaveTypeListsQueryHandlerTests()
	{
		_mockRepo = MockLeaveTypeRepository.GetLeaveTypesMockLeaveTypeRepository();

		var mappConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<LeaveTypeProfile>();
		});

		_mapper = mappConfig.CreateMapper();
		_appLogger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
	}

	[Fact]
	public async Task GetLeaveTypeListTest()
	{
		var handler = new GetLeaveTypesQueryHandler(_mapper,_mockRepo.Object,_appLogger.Object);

		var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

		result.ShouldBeOfType<List<LeaveTypeDto>>();
		result.Count.ShouldBe(3);
	}
}
