using AutoMapper;
using HR.LeaveManagement.UI.BlazorUI.Models;
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

		CreateMap<LeaveRequestListDto, LeaveRequestVM>()
			.ForMember(q => q.DateRequest, opt => opt.MapFrom(x => x.DateRequested.DateTime))
			.ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
			.ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
			.ForMember(q => q.LeaveType, opt => opt.MapFrom(x => x.LeaveTypeDto))
			.ForMember(q => q.Approved, opt => opt.MapFrom(x => x.Approval))
			.ReverseMap();
		CreateMap<LeaveRequestDetailsDto, LeaveRequestVM>()
			.ForMember(q => q.DateRequest, opt => opt.MapFrom(x => x.DateTimeRequested.DateTime))
			.ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
			.ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
			.ForMember(q => q.LeaveType, opt => opt.MapFrom(x => x.LeaveTypeDto))
			.ReverseMap();

		CreateMap<EmployeeVM, Employee>().ReverseMap();

	}
}
