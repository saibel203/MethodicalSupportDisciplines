using System.Text;
using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.BLL.Models.Identity;
using MethodicalSupportDisciplines.Core.IOptions;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Dto;
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
    private readonly DataDbContext _dbContext;
    private readonly IMailService _mailService;
    private readonly WebPathsOptions _webPathsOptions;
    private readonly ILogger<AuthService> _logger;
    private readonly IMapper _mapper;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        DataDbContext dbContext, IOptions<WebPathsOptions> webPathsOptions, ILogger<AuthService> logger, IMapper mapper, 
        IMailService mailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dbContext = dbContext;
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
                         $"?userId={user.Id}&token={validEmailToken}";
        
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

    public async Task<UserAuthResponse> ConfirmEmailAsync(string userId, string token)
    {
        try
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return new UserAuthResponse
                {
                    Message = "No user found with this ID",
                    IsSuccess = false
                };
            }
        
            byte[] decodedToken = WebEncoders.Base64UrlDecode(token);
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