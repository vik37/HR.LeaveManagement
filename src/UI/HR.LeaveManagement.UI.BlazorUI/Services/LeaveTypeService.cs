﻿using AutoMapper;
using Blazored.LocalStorage;
using HR.LeaveManagement.UI.BlazorUI.Contracts;
using HR.LeaveManagement.UI.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.UI.BlazorUI.Services.Base;

namespace HR.LeaveManagement.UI.BlazorUI.Services;

public class LeaveTypeService : BaseHttpService, ILeaveTypeService
{
	private readonly IMapper _mapper;

	public LeaveTypeService(IClient client, ILocalStorageService localStorageService, IMapper mapper) 
		: base(client, localStorageService)
	{
		_mapper = mapper;
	}

	public async Task<List<LeaveTypeVM>> GetLeaveTypes()
	{
		var leaveTypes = await _client.LeaveTypeAllAsync();
		return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
	}

	public async Task<LeaveTypeVM> GetLeaveType(int id)
	{
		var leaveType = await _client.LeaveTypeGETAsync(id);
		return _mapper.Map<LeaveTypeVM>(leaveType);
	}

	public async Task<LeaveTypeDetailVM> GetLeaveTypeDetails(int id)
	{
		var leaveTypeDetails = await _client.LeaveTypeGETAsync(id);
		return _mapper.Map<LeaveTypeDetailVM>(leaveTypeDetails);
	}

	public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM model)
	{
		try
		{
			var createLeaveTypeCommand = _mapper.Map<CreateLeaveTypeCommand>(model);
			await _client.LeaveTypePOSTAsync(createLeaveTypeCommand);
			return new Response<Guid>();
		}
		catch (ApiException ex)
		{
			return ConvertApiExceptions(ex);
		}
	}

	public async Task<Response<Guid>> UpdateLeaveType(LeaveTypeVM model)
	{
		try
		{
			var updateLeaveTypeCommand = _mapper.Map<UpdateLeaveTypeCommand>(model);
			await _client.LeaveTypePUTAsync(updateLeaveTypeCommand);
			return new Response<Guid>();
		}
		catch (ApiException ex)
		{
			return ConvertApiExceptions(ex);
		}
	}

	public async Task<Response<Guid>> DeleteLeaveType(int id)
	{
		try
		{
			await _client.LeaveTypeDELETEAsync(id);
			return new Response<Guid>();
		}
		catch (ApiException ex)
		{
			return ConvertApiExceptions(ex);
		}
	}
}
