namespace HR.LeaveManager.Application.Exceptions;

public class NotFoundException : Exception
{
	public NotFoundException(string name, object key) : base(message: $"{name} ({key}) was not found")
	{ }
}
