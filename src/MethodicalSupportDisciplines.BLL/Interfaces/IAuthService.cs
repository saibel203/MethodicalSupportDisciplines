using MethodicalSupportDisciplines.Shared.Dto;
using MethodicalSupportDisciplines.Shared.Responses.Services;

namespace MethodicalSupportDisciplines.BLL.Interfaces;

public interface IAuthService
{
    Task<UserAuthResponse> RegisterAsync(UserRegisterDto? userRegisterDto);
    Task<UserAuthResponse> LoginAsync(UserLoginDto? userLoginDto);
    Task<UserAuthResponse> ConfirmEmailAsync(string userId, string token);
    Task LogoutAsync();
}