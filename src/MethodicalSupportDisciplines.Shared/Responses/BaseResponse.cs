﻿namespace MethodicalSupportDisciplines.Shared.Responses;

public class BaseResponse
{
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
}