﻿namespace ZeroGravity.Services.Authorization.Dto;

public class UpdateUserDto
{
    public string? NewUserName { get; set; }
    public string? Email { get; set; }
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
}