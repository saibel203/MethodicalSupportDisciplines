using System.Text;
using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.Core.Entities.Users;
using MethodicalSupportDisciplines.Core.Models.Identity;
using MethodicalSupportDisciplines.Core.IOptions;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Dto;
using MethodicalSupportDisciplines.Shared.Dto.AuthDto;
using MethodicalSupportDisciplines.Shared.Responses.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MethodicalSupportDisciplines.BLL.Services;

public class AuthService : IAuthService
{
    private readonly IStringLocalizer<AuthService> _stringLocalization;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly DataDbContext _dbContext;
    private readonly IMailService _mailService;
    private readonly WebPathsOptions _webPathsOptions;
    private readonly ILogger<AuthService> _logger;
    private readonly IMapper _mapper;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        RoleManager<ApplicationRole> roleManager, IOptions<WebPathsOptions> webPathsOptions, 
        ILogger<AuthService> logger, IMapper mapper, IMailService mailService, DataDbContext dbContext,
        IStringLocalizer<AuthService> stringLocalization)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _webPathsOptions = webPathsOptions.Value;
        _logger = logger;
        _mapper = mapper;
        _mailService = mailService;
        _dbContext = dbContext;
        _stringLocalization = stringLocalization;
    }

    public async Task<UserAuthResponse> RegisterAsync(UserRegisterDto? userRegisterDto)
    {
        try
        {
            if (userRegisterDto is null)
            {
                return new UserAuthResponse
                {
                    Message = _stringLocalization["MethodGetIncorrectData"],
                    IsSuccess = false
                };
            }

            ApplicationUser user = _mapper.Map<ApplicationUser>(userRegisterDto);
            IdentityResult createUserResult = await _userManager.CreateAsync(user, userRegisterDto.Password);

            if (!createUserResult.Succeeded)
            {
                return new UserAuthResponse
                {
                    Message = _stringLocalization["ErrorTryCreateAccount"],
                    IsSuccess = false,
                    Errors = createUserResult.Errors
                };
            }

            if (user.Email is null)
            {
                return new UserAuthResponse
                {
                    Message = _stringLocalization["UserWithoutEmail"],
                    IsSuccess = false,
                };
            }

            const string guestUserRoleName = "guest";
            ApplicationRole? guestUserRole = await _roleManager.Roles
                .FirstOrDefaultAsync(roleData => roleData.Name == guestUserRoleName);

            if (guestUserRole?.Name is null)
            {
                return new UserAuthResponse
                {
                    Message = _stringLocalization["ErrorGetRole"],
                    IsSuccess = false
                };
            }
            
            await _userManager.AddToRoleAsync(user, guestUserRole.Name);
            
            string confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            byte[] encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
            string validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            string url = $"{_webPathsOptions.WebMvcApplicationUrl}/auth/confirmEmailResult" +
                         $"?value={user.Id}&token={validEmailToken}";
        
            await _mailService.SendEmailAsync(user.Email, "Підтвердження email", 
                "Підтвердження email на сайті MethodicalSupportDisciplines. Якщо ви не реєструвались, " +
                    "проігноруйте цей лист.", 
                url, "Натисніть для підтвердження");

            GuestUser guestUser = new GuestUser
            {
                ApplicationUser = user,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Patronymic = userRegisterDto.Patronymic
            };
            
            await _dbContext.GuestUsers.AddAsync(guestUser);
            await _dbContext.SaveChangesAsync();
            
            return new UserAuthResponse
            {
                Message = _stringLocalization["RegisterSuccess"],
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to register a new user.");

            return new UserAuthResponse
            {
                Message = _stringLocalization["UnknownRegisterError"],
                IsSuccess = false
            };
        }
    }

    public async Task<UserAuthResponse> LoginAsync(UserLoginDto? userLoginDto)
    {
        try
        {
            if (userLoginDto is null)
            {
                return new UserAuthResponse
                {
                    Message = _stringLocalization["MethodGetIncorrectData"],
                    IsSuccess = false
                };
            }
            
            ApplicationUser? user = await _userManager.FindByEmailAsync(userLoginDto.Email);

            if (user is null)
            {
                return new UserAuthResponse
                {
                    Message = _stringLocalization["UserWithEmailNotFound"],
                    IsSuccess = false
                };
            }
            
            SignInResult loginUserResult = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, 
                userLoginDto.RememberMe, false);

            if (!loginUserResult.Succeeded)
            {
                if (!user.EmailConfirmed)
                {
                    return new UserAuthResponse
                    {
                        Message = _stringLocalization["ConfirmEmailError"],
                        IsSuccess = false
                    };
                }
                
                return new()
                {
                    Message = _stringLocalization["PasswordForEmailIncorrect"],
                    IsSuccess = false
                };
            }
            
            return new()
            {
                Message = _stringLocalization["LoginSuccess"],
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to log the user in.");

            return new UserAuthResponse
            {
                Message = _stringLocalization["LoginUnknownError"],
                IsSuccess = false
            };
        }
    }

    public async Task<UserAuthResponse> ConfirmEmailAsync(TokenValueDto<string>? tokenValueDto)
    {
        try
        {
            if (tokenValueDto?.Value is null)
            {
                return new UserAuthResponse
                {
                    Message = _stringLocalization["MethodGetIncorrectData"],
                    IsSuccess = false
                };
            }
            
            ApplicationUser? user = await _userManager.FindByIdAsync(tokenValueDto.Value);

            if (user is null)
            {
                return new UserAuthResponse
                {
                    Message = _stringLocalization["NoUserWithId"],
                    IsSuccess = false
                };
            }
        
            byte[] decodedToken = WebEncoders.Base64UrlDecode(tokenValueDto.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
        
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (!result.Succeeded)
            {
                return new()
                {
                    Message = _stringLocalization["ConfirmEmailUnknownError"],
                    IsSuccess = false,
                    Errors = result.Errors
                };
            }

            return new()
            {
                Message = _stringLocalization["ConfirmSuccess"],
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to confirm Email.");
            
            return new UserAuthResponse
            {
                Message = _stringLocalization["ConfirmError"],
                IsSuccess = false
            };
        }
    }

    public async Task<UserAuthResponse> RemindPasswordAsync(RemindPasswordDto? remindPasswordDto)
    {
        try
        {
            if (remindPasswordDto is null)
            {
                return new UserAuthResponse
                {
                    Message = _stringLocalization["MethodGetIncorrectData"],
                    IsSuccess = false
                };
            }
            
            ApplicationUser? user = await _userManager.FindByEmailAsync(remindPasswordDto.Email);

            if (user is null)
            {
                return new UserAuthResponse
                {
                    Message = _stringLocalization["UserWithEmailNotFound"],
                    IsSuccess = false
                };
            }
            
            string defaultToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            byte[] encodedToken = Encoding.UTF8.GetBytes(defaultToken);
            string currentToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_webPathsOptions.WebMvcApplicationUrl}/Auth/ResetPassword?" +
                         $"value={remindPasswordDto.Email}&token={currentToken}";
        
            await _mailService.SendEmailAsync(remindPasswordDto.Email, "Відновлення паролю",
                "Відновлення паролю на сайті WorkForYou. Якщо ви нічого не змінювали, проігноруйте це повідомлення.",
                url, "Відновлення паролю");
        
            return new UserAuthResponse
            {
                Message = _stringLocalization["RemindSuccess"],
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to send an email to " +
                                 "change the password.");

            return new UserAuthResponse
            {
                Message = _stringLocalization["RemindUnknownError"],
                IsSuccess = false
            };
        }
    }

    public async Task<UserAuthResponse> ResetPasswordAsync(ResetPasswordDto? resetPasswordDto)
    {
        try
        {
            if (resetPasswordDto is null)
            {
                return new UserAuthResponse
                {
                    Message = _stringLocalization["MethodGetIncorrectData"],
                    IsSuccess = false
                };
            }
            
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

            if (user is null)
            {
                return new UserAuthResponse
                {
                    Message = _stringLocalization["UserWithEmailNotFound"],
                    IsSuccess = false
                };
            }

            if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmNewPassword)
            {
                return new()
                {
                    Message = _stringLocalization["PasswordsMatchError"],
                    IsSuccess = false
                };
            }
            
            byte[] decodedToken = WebEncoders.Base64UrlDecode(resetPasswordDto.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            
            IdentityResult resetPasswordResult = await _userManager.ResetPasswordAsync(user, normalToken, resetPasswordDto.NewPassword);

            if (!resetPasswordResult.Succeeded)
            {
                return new()
                {
                    Message = _stringLocalization["ResetPasswordError"],
                    IsSuccess = false,
                    Errors = resetPasswordResult.Errors
                };
            }

            await _mailService.SendEmailAsync(resetPasswordDto.Email, "Змінено паролю", 
                $"На сайті WorkForYou ваш пароль було змінено на {resetPasswordDto.NewPassword}. " +
                $"Якщо ви нічого не змінювали, проігноруйте цей лист.",
                "", "Перейти на сайт");

            return new()
            {
                Message = _stringLocalization["ResetPasswordSuccess"],
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "");

            return new UserAuthResponse
            {
                Message = _stringLocalization["ResetPasswordUnknownError"],
                IsSuccess = false
            };
        }
    }

    public async Task LogoutAsync()
    {
        try
        {
            await _signInManager.SignOutAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to log the user out.");
        }
    }
}