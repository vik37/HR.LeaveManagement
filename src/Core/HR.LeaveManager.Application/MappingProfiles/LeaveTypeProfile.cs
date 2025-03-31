using AutoMapper;
using HR.LeaveManagement.Domain;
using HR.LeaveManager.Application.Feature.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManager.Application.Feature.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManager.Application.Feature.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManager.Application.Feature.LeaveType.Queries.GetLeaveTypeDetail;

namespace HR.LeaveManager.Application.MappingProfiles;

public class LeaveTypeProfile : Profile
{
	public LeaveTypeProfile()
	{
		CreateMap<LeaveTypeDto, LeaveType>()
			.ReverseMap();
		CreateMap<LeaveTypeDetailsDto, LeaveType>()
			.ForMember(q => q.ModifiedBy, opt => opt.Ignore())
			.ForMember(q => q.CreatedBy, opt => opt.Ignore())
			.ReverseMap()
			.ForMember(q => q.ModifiedBy, opt => opt.Ignore())
			.ForMember(q => q.CreatedBy, opt => opt.Ignore());
			
		CreateMap<CreateLeaveTypeCommand, LeaveType>();
		CreateMap<UpdateLeaveTypeCommand, LeaveType>();
	}
}
