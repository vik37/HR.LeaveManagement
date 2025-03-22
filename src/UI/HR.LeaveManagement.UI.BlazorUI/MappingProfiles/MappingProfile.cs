using AutoMapper;
using HR.LeaveManagement.UI.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.UI.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.UI.BlazorUI.Services.Base;

namespace HR.LeaveManagement.UI.BlazorUI.MappingProfiles;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
		CreateMap<CreateLeaveTypeCommand,LeaveTypeVM>().ReverseMap();
		CreateMap<UpdateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();

		CreateMap<CreateLeaveRequestCommand, LeaveRequestVM>().ReverseMap();
		CreateMap<UpdateLeaveRequestCommand, LeaveRequestVM>().ReverseMap();

	}
}
