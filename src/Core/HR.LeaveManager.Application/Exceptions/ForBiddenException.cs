namespace HR.LeaveManager.Application.Exceptions;

public class ForBiddenException : Exception
{
	public ForBiddenException(string message): base(message) { }
}
