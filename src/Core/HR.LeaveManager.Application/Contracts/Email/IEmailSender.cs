using HR.LeaveManager.Application.Models;

namespace HR.LeaveManager.Application.Contracts.Email;

public interface IEmailSender
{
	Task<bool> SendEmail(EmailMessage email);
}
