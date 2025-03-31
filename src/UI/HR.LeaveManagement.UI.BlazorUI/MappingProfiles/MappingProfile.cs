using AutoMapper;
using HR.LeaveManagement.UI.BlazorUI.Models;
using HR.LeaveManagement.UI.BlazorUI.Models.LeaveAllocations;
using HR.LeaveManagement.UI.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.UI.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.UI.BlazorUI.Services.Base;

namespace HR.LeaveManagement.UI.BlazorUI.MappingProfiles;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
		CreateMap<LeaveTypeDetailsDto, LeaveTypeDetailVM>()
			.ForMember(q => q.DateCreated, opt => opt.MapFrom(x => x.DateCreated.DateTime))
			.ForMember(q => q.DateModified, opt => opt.MapFrom(x => x.DateModified.DateTime))
			.ReverseMap()
			.ForMember(q => q.DateCreated, opt => opt.MapFrom(x => new DateTimeOffset(x.DateCreated)))
			.ForMember(q => q.DateModified, opt => opt.MapFrom(x => new DateTimeOffset(x.DateModified)));

		CreateMap<LeaveTypeDetailsDto, LeaveTypeVM>()
			.ReverseMap()
			.ForMember(q => q.CreatedBy, opt => opt.Ignore())
			.ForMember(q => q.ModifiedBy, opt => opt.Ignore())
			.ForMember(q => q.DateCreated, opt => opt.Ignore())
			.ForMember(q => q.DateModified, opt => opt.Ignore());

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
			.ForMember(q => q.Cancelled, opt => opt.MapFrom(x => x.Cancelled))
			.ReverseMap();
		CreateMap<LeaveRequestDetailsDto, LeaveRequestVM>()
			.ForMember(q => q.DateRequest, opt => opt.MapFrom(x => x.DateTimeRequested.DateTime))
			.ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
			.ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
			.ForMember(q => q.LeaveType, opt => opt.MapFrom(x => x.LeaveTypeDto))
			.ReverseMap();

		CreateMap<LeaveAllocationDto, LeaveAllocationVM>().ReverseMap();
		CreateMap<LeaveAllocationDetailsDto, LeaveAllocationVM>().ReverseMap();

		CreateMap<EmployeeVM, Employee>().ReverseMap();

	}
}
