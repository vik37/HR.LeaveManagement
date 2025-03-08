using AutoMapper;
using HR.LeaveManagement.Domain;
using HR.LeaveManager.Application.Feature.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManager.Application.Feature.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManager.Application.Feature.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManager.Application.Feature.LeaveAllocation.Queries.GetLeaveAllocations;

namespace HR.LeaveManager.Application.MappingProfiles;

public class LeaveAllocationProfile : Profile
{
	public LeaveAllocationProfile()
	{
		CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
		CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();
		CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
		CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>();
	}
}
