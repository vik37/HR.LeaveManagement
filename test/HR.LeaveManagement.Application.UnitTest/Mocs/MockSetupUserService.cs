using HR.LeaveManager.Application.Contracts.Identity;
using HR.LeaveManager.Application.Models.Identities;
using Moq;

namespace HR.LeaveManagement.Application.UnitTest.Mocs
{
    public static class MockUserService
    {
		public const string UserId = "7fc521c9-ed08-402d-8543-19d250d12f34";
        public const string Email = "test@someone.com";

		public static Mock<IUserService> GetMockedUserLoggedCredentials(string role = "Administrator")
        {
            var mockUserService = new Mock<IUserService>();

            var user = new List<Employee>
            {
                new Employee
                {
                    Id = UserId,
                    Email = Email,
                    Firstname = "Test",
                    Lastname = "Testing",
                },
                new Employee
                {
                    Id ="70c67b77-7c60-4c3c-b7bb-1b84c651a5c3",
                    Email =  "another@gmail.com",
                    Firstname = "Another",
                    Lastname = "Test",
                }
            };

            mockUserService.SetupGet(x => x.Role).Returns(role);
            mockUserService.SetupGet(x => x.UserId).Returns(UserId);
            mockUserService.Setup(x => x.GetEmployeeById(It.IsAny<string>())).ReturnsAsync((string id) => user.First(x => x.Id == UserId));

            return mockUserService;
        }
    }
}
