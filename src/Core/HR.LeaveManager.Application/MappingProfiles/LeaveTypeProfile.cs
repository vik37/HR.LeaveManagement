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
		CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
		CreateMap<LeaveType, LeaveTypeDetailsDto>().ReverseMap();
		CreateMap<CreateLeaveTypeCommand, LeaveType>();
		CreateMap<UpdateLeaveTypeCommand, LeaveType>();
	}
}
