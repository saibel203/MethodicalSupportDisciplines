using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.BLL.Models.Identity;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Dto;
using MethodicalSupportDisciplines.Shared.Responses.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.BLL.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly DataDbContext _dbContext;
    private readonly ILogger<AuthService> _logger;
    private readonly IMapper _mapper;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        DataDbContext dbContext, ILogger<AuthService> logger, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
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
            
            // await _userManager.AddToRoleAsync(user, userRegisterDto.SelectedRole); for future
            
            return new UserAuthResponse
            {
                Message = "User created successfully",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"An unknown error occurred while trying to register a new user: {ex.Message}");

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
            _logger.LogError($"An unknown error occurred while trying to log the user in: {ex.Message}");

            return new UserAuthResponse
            {
                Message = "An unknown error occurred while trying to log the user in",
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
            _logger.LogError($"An unknown error occurred while trying to log the user out: {ex.Message}");
        }
    }
}