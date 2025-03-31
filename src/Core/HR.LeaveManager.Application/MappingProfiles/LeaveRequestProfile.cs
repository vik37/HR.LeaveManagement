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
			.ForMember(q => q.ModifiedBy, opt => opt.Ignore())
			.ForMember(q => q.CreatedBy, opt => opt.Ignore())
			.ForMember(src => src.LeaveType, dest => dest.MapFrom(x => x.LeaveTypeDto))
			.ForMember(src => src.Approved, dest => dest.MapFrom(x => x.Approval)).ReverseMap();
		CreateMap<LeaveRequestDetailsDto, LeaveRequest>()
			.ForMember(q => q.ModifiedBy, opt => opt.Ignore())
			.ForMember(q => q.CreatedBy, opt => opt.Ignore())
			.ForMember(src => src.DateRequested, dest => dest.MapFrom(x => x.DateTimeRequested))
			.ForMember(src => src.LeaveType, dest => dest.MapFrom(x => x.LeaveTypeDto)).ReverseMap();
		CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
		CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
	}
}
