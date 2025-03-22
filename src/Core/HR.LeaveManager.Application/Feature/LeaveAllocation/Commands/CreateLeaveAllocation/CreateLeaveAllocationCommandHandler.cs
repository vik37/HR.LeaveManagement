using AutoMapper;
using HR.LeaveManager.Application.Contracts.Identity;
using HR.LeaveManager.Application.Contracts.Persistence;
using HR.LeaveManager.Application.Exceptions;
using MediatR;

namespace HR.LeaveManager.Application.Feature.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
	private readonly ILeaveAllocationRepository _leaveAllocationRepository;
	private readonly ILeaveTypeRepository _leaveTypeRepository;
	private readonly IMapper _mapper;

	private readonly IUserService _userService;

	public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository,
		ILeaveTypeRepository leaveTypeRepository, IMapper mapper, IUserService userService)
	{
		_leaveAllocationRepository = leaveAllocationRepository;
		_leaveTypeRepository = leaveTypeRepository;
		_mapper = mapper;
		_userService = userService;
	}

	public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
	{
		var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
		var validationResult = await validator.ValidateAsync(request);

		if (validationResult.Errors.Any())
			throw new BadRequestException("Invalid Leave Allocation Request", validationResult);

		var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

		// Get Employees
		var employees = await _userService.GetAllEmployees();

		// Get Period
		var period = DateTime.Now.Year;

		// Assigned Allocation if allocation doen't allready exist for period and leave type
		var allocations = new List<LeaveManagement.Domain.LeaveAllocation>();
		foreach (var emp in employees)
		{
			var allocationExists = await _leaveAllocationRepository.AllocationExists(emp.Id, leaveType.Id, period);

			if(allocationExists == false)
			{
				allocations.Add(new LeaveManagement.Domain.LeaveAllocation
				{
					EmployeeId = emp.Id,
					LeaveTypeId = leaveType.Id,
					NumberOfDays = leaveType.DefaultDays,
					Period = period
				});
			}
		}

		if(allocations.Any())
			await _leaveAllocationRepository.AddAllocation(allocations);

		return Unit.Value;
	}
}
