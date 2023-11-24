using Microsoft.AspNetCore.Identity;

namespace MethodicalSupportDisciplines.Shared.Responses.Services;

public class UserAuthResponse : BaseResponse
{
    public IEnumerable<IdentityError>? Errors { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
}