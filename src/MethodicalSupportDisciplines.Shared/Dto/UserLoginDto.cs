﻿namespace MethodicalSupportDisciplines.Shared.Dto;

public class UserLoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; }
}