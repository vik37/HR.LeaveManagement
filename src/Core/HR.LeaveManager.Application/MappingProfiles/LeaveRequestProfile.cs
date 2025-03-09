using AutoMapper;
using HR.LeaveManagement.Domain;
using HR.LeaveManager.Application.Feature.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManager.Application.Feature.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManager.Application.Feature.LeaveRequest.Queries.GetLeaveRequestDetails;
using HR.LeaveManager.Application.Feature.LeaveRequest.Queries.GetLeaveRequestList;

namespace HR.LeaveManager.Application.MappingProfiles;

public class LeaveRequestProfile : Profile
{
	public LeaveRequestProfile()
	{
		CreateMap<LeaveRequestListDto, LeaveRequest>().ReverseMap();
		CreateMap<LeaveRequest, LeaveRequestDetailsDto>();
		CreateMap<CreateLeaveRequestCommand, LeaveAllocation>();
		CreateMap<UpdateLeaveRequestCommand, LeaveAllocation>();
	}
}
