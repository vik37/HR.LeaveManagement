using AutoMapper;
using HR.LeaveManagement.UI.BlazorUI.Models;
using HR.LeaveManagement.UI.BlazorUI.Services.Base;

namespace HR.LeaveManagement.UI.BlazorUI.MappingProfiles;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
		CreateMap<CreateLeaveTypeCommand,LeaveTypeVM>().ReverseMap();
		CreateMap<UpdateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();
	}
}
