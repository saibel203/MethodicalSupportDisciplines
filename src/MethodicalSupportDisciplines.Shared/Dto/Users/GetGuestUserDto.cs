namespace MethodicalSupportDisciplines.Shared.Dto.Users;

public class GetGuestUserDto : BaseUserDto
{
    public int GuestUserId { get; set; }
    public string ApplicationUserId { get; set; } = string.Empty;
}