using MethodicalSupportDisciplines.Shared.Dto;
using MethodicalSupportDisciplines.Shared.Dto.AuthDto;
using MethodicalSupportDisciplines.Shared.Responses.Services;

namespace MethodicalSupportDisciplines.BLL.Interfaces;

public interface IAuthService
{
    Task<UserAuthResponse> RegisterAsync(UserRegisterDto? userRegisterDto);
    Task<UserAuthResponse> LoginAsync(UserLoginDto? userLoginDto);
    Task<UserAuthResponse> ConfirmEmailAsync(TokenValueDto<string>? tokenValueDto);
    Task<UserAuthResponse> RemindPasswordAsync(RemindPasswordDto? remindPasswordDto);
    Task<UserAuthResponse> ResetPasswordAsync(ResetPasswordDto? resetPasswordDto);
    Task LogoutAsync();
}