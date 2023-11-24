﻿using System.Text;
using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.BLL.Models.Identity;
using MethodicalSupportDisciplines.Core.IOptions;
using MethodicalSupportDisciplines.Shared.Dto;
using MethodicalSupportDisciplines.Shared.Dto.AuthDto;
using MethodicalSupportDisciplines.Shared.Responses.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MethodicalSupportDisciplines.BLL.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMailService _mailService;
    private readonly WebPathsOptions _webPathsOptions;
    private readonly ILogger<AuthService> _logger;
    private readonly IMapper _mapper;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
        IOptions<WebPathsOptions> webPathsOptions, ILogger<AuthService> logger, IMapper mapper, 
        IMailService mailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _webPathsOptions = webPathsOptions.Value;
        _logger = logger;
        _mapper = mapper;
        _mailService = mailService;
    }

    public async Task<UserAuthResponse> RegisterAsync(UserRegisterDto? userRegisterDto)
    {
        try
        {
            if (userRegisterDto is null)
            {
                return new UserAuthResponse
                {
                    Message = "The method received an incorrect value, possibly null.",
                    IsSuccess = false
                };
            }

            ApplicationUser user = _mapper.Map<ApplicationUser>(userRegisterDto);
            IdentityResult createUserResult = await _userManager.CreateAsync(user, userRegisterDto.Password);

            if (!createUserResult.Succeeded)
            {
                return new UserAuthResponse
                {
                    Message = "Create user error",
                    IsSuccess = false,
                    Errors = createUserResult.Errors
                };
            }

            if (user.Email is null)
            {
                return new UserAuthResponse
                {
                    Message = "The user does not have an Email",
                    IsSuccess = false,
                };
            }
            
            // await _userManager.AddToRoleAsync(user, userRegisterDto.SelectedRole); for future
            
            string confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            byte[] encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
            string validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            string url = $"{_webPathsOptions.WebMvcApplicationUrl}/auth/confirmEmailResult" +
                         $"?value={user.Id}&token={validEmailToken}";
        
            await _mailService.SendEmailAsync(user.Email, "Підтвердження email", 
                "Підтвердження email на сайті MethodicalSupportDisciplines. Якщо ви не реєструвались, " +
                    "проігноруйте цей лист.", 
                url, "Натисніть для підтвердження");
            
            return new UserAuthResponse
            {
                Message = "User created successfully",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to register a new user.");

            return new UserAuthResponse
            {
                Message = "An unknown error occurred while trying to register a new user",
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
                    Message = "The method received an incorrect value, possibly null",
                    IsSuccess = false
                };
            }
            
            ApplicationUser? user = await _userManager.FindByEmailAsync(userLoginDto.Email);

            if (user is null)
            {
                return new UserAuthResponse
                {
                    Message = "The user was not found according to the entered data",
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
                        Message = "Before login, confirm your Email",
                        IsSuccess = false
                    };
                }
                
                return new()
                {
                    Message = "Password error",
                    IsSuccess = false
                };
            }
            
            return new()
            {
                Message = "The user has successfully logged in",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to log the user in.");

            return new UserAuthResponse
            {
                Message = "An unknown error occurred while trying to log the user in",
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
                    Message = "",
                    IsSuccess = false
                };
            }
            
            ApplicationUser? user = await _userManager.FindByIdAsync(tokenValueDto.Value);

            if (user is null)
            {
                return new UserAuthResponse
                {
                    Message = "No user found with this ID",
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
                    Message = "Confirm your email first",
                    IsSuccess = false,
                    Errors = result.Errors
                };
            }

            return new()
            {
                Message = "Email has been successfully verified",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to confirm Email.");
            
            return new UserAuthResponse
            {
                Message = "An unknown error occurred while trying to confirm Email",
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
                    Message = "The method received an incorrect value, possibly null",
                    IsSuccess = false
                };
            }
            
            ApplicationUser? user = await _userManager.FindByEmailAsync(remindPasswordDto.Email);

            if (user is null)
            {
                return new UserAuthResponse
                {
                    Message = "User with email not found",
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
                Message = "Success",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "");

            return new UserAuthResponse
            {
                Message = "",
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
                    Message = "",
                    IsSuccess = false
                };
            }
            
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

            if (user is null)
            {
                return new UserAuthResponse
                {
                    Message = "",
                    IsSuccess = false
                };
            }

            if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmNewPassword)
            {
                return new()
                {
                    Message = "",
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
                    Message = "",
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
                Message = "",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "");

            return new UserAuthResponse
            {
                Message = "",
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