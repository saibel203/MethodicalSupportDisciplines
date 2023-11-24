namespace MethodicalSupportDisciplines.Shared.Dto;

public class TokenValueDto<T>
{
    public string Token { get; set; } = string.Empty;
    public T? Value { get; set; }
}