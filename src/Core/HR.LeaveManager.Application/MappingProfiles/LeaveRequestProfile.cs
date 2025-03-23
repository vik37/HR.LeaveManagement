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
		CreateMap<LeaveRequestListDto, LeaveRequest>()
			.ForMember(src => src.LeaveType, dest => dest.MapFrom(x => x.LeaveTypeDto)).ReverseMap();
		CreateMap<LeaveRequestDetailsDto, LeaveRequest>().ReverseMap();
		CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
		CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
	}
}
