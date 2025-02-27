﻿namespace HR.LeaveManager.Application.Models;

public class EmailSettings
{
	public string APIKey { get; set; }	= string.Empty;
	public string FromAddress { get; set; } = string.Empty;
	public string FromName { get; set; } = string.Empty;
}
