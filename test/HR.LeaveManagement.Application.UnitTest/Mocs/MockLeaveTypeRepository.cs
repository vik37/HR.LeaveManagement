using HR.LeaveManagement.Domain;
using HR.LeaveManager.Application.Contracts.Persistence;
using Moq;

namespace HR.LeaveManagement.Application.UnitTest.Mocs;

public class MockLeaveTypeRepository
{
	public static Mock<ILeaveTypeRepository> GetLeaveTypesMockLeaveTypeRepository()
	{
		int count = 0;
		var leaveTypes = new List<LeaveType>()
		{
			new LeaveType
			{
				Id = 1,
				Name = "Test Vacation",
				DefaultDays = 10
			},
			new LeaveType
			{
				Id = 2,
				Name = "Test Sick",
				DefaultDays = 15
			},
			new LeaveType
			{
				Id = 3,
				Name = "Test Maternity",
				DefaultDays = 15
			}
		};

		var mockRepo = new Mock<ILeaveTypeRepository>();

		mockRepo.Setup(r => r.GetAsynt()).ReturnsAsync(leaveTypes);
		mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => leaveTypes.FirstOrDefault(l => l.Id == id));
		mockRepo.Setup(r => r.IsLeaveTypeUnique(It.IsAny<string>())).ReturnsAsync((string name) => leaveTypes.Any(x => x.Name == name) == false);

		mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
			.Returns((LeaveType leaveType) =>
			{
				leaveType.Id = leaveTypes.Count+1;
				leaveTypes.Add(leaveType);
				return Task.CompletedTask;
			});

		return mockRepo;
	}
}
