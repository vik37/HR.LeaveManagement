﻿using Blazored.LocalStorage;
using HR.LeaveManagement.UI.BlazorUI.Contracts;
using HR.LeaveManagement.UI.BlazorUI.Services.Base;

namespace HR.LeaveManagement.UI.BlazorUI.Services;

public class LeaveRequestService : BaseHttpService, ILeaveRequestService
{
	public LeaveRequestService(IClient client, ILocalStorageService localStorageService) : base(client, localStorageService)
	{
	}
}
