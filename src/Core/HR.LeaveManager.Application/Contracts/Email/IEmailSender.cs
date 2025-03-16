using HR.LeaveManager.Application.Models.Emails;

namespace HR.LeaveManager.Application.Contracts.Email;

public interface IEmailSender
{
	Task<bool> SendEmail(EmailMessage email);
}
