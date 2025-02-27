using FluentValidation.Results;

namespace HR.LeaveManager.Application.Exceptions;

public class BadRequestException : Exception
{
	public BadRequestException(string message) : base(message)
	{ }

	public BadRequestException(string message, ValidationResult validationResult) : base(message)
	{
		foreach (var error in validationResult.Errors)
		{
			ValidationErrors.Add(error.ErrorMessage);
		}
	}

	public List<string> ValidationErrors { get; set; } = new();
}
