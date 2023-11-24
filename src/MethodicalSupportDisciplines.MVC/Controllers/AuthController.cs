using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.Shared.Dto;
using MethodicalSupportDisciplines.Shared.Dto.AuthDto;
using MethodicalSupportDisciplines.Shared.Responses.Services;
using MethodicalSupportDisciplines.Shared.ViewModels.Forms.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MethodicalSupportDisciplines.MVC.Controllers;

public class AuthController : Controller
{
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;

    public AuthController(IMapper mapper, IAuthService authService)
    {
        _mapper = mapper;
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(registerViewModel);
        }
        
        UserRegisterDto registerModelDto = _mapper.Map<UserRegisterDto>(registerViewModel);
        UserAuthResponse registerResult = await _authService.RegisterAsync(registerModelDto);

        if (!registerResult.IsSuccess)
        {
            if (registerResult.Errors is not null)
            {
                foreach (IdentityError error in registerResult.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
            }
            else
            {
                ModelState.AddModelError("", registerResult.Message);
            }

            return View(registerViewModel);
        }
        
        return RedirectToAction(nameof(ConfirmEmail));
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel, string? returnUrl)
    {
        returnUrl ??= Url.Content("~/");
        
        if (!ModelState.IsValid)
        {
            return View(loginViewModel);
        }

        UserLoginDto loginModelDto = _mapper.Map<UserLoginDto>(loginViewModel);
        UserAuthResponse loginResult = await _authService.LoginAsync(loginModelDto);
        
        if (!loginResult.IsSuccess)
        {
            ModelState.AddModelError("", loginResult.Message);
            return View(loginViewModel);
        }
        
        return RedirectToLocal(returnUrl);
    }

    [HttpGet]
    public IActionResult ConfirmEmail()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmailResult([FromQuery] TokenValueDto<string> tokenValueDto)
    {
        if (tokenValueDto.Value is null || 
            string.IsNullOrWhiteSpace(tokenValueDto.Value) || string.IsNullOrWhiteSpace(tokenValueDto.Token))
        {
            return RedirectToAction(nameof(Login));
        }
        
        UserAuthResponse result = await _authService.ConfirmEmailAsync(tokenValueDto);
        
        if (!result.IsSuccess)
        {
            if (result.Errors is not null)
            {
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
            }
            else
            {
                ModelState.AddModelError("", result.Message);
            }

            return View();
        }

        return View();
    }
    
    public IActionResult RemindPasswordResult()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult RemindPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemindPassword(RemindPasswordViewModel forgetPasswordViewModel)
    {
        if (!ModelState.IsValid || string.IsNullOrWhiteSpace(forgetPasswordViewModel.Email))
        {
            return View(forgetPasswordViewModel);
        }
        
        RemindPasswordDto forgetPasswordDto = _mapper.Map<RemindPasswordDto>(forgetPasswordViewModel);
        UserAuthResponse forgetPasswordResult = await _authService.RemindPasswordAsync(forgetPasswordDto);
        
        if (!forgetPasswordResult.IsSuccess)
        {
            ModelState.AddModelError("", forgetPasswordResult.Message);
            return View(forgetPasswordViewModel);
        }
        
        return RedirectToAction(nameof(RemindPasswordResult));
    }
    
    [HttpGet]
    public IActionResult ResetPasswordResult()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult ResetPassword([FromQuery] TokenValueDto<string> tokenValueDto)
    {
        if (tokenValueDto.Value is null 
            || string.IsNullOrWhiteSpace(tokenValueDto.Value) || string.IsNullOrWhiteSpace(tokenValueDto.Token))
        {
            return RedirectToAction(nameof(Login));
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(resetPasswordViewModel);
        }
        
        ResetPasswordDto resetPasswordDto = _mapper.Map<ResetPasswordDto>(resetPasswordViewModel);
        UserAuthResponse resetPasswordResult = await _authService.ResetPasswordAsync(resetPasswordDto);
        
        if (!resetPasswordResult.IsSuccess)
        {
            if (resetPasswordResult.Errors is not null)
            {
                foreach (IdentityError error in resetPasswordResult.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
            }
            else
            {
                ModelState.AddModelError("", resetPasswordResult.Message);
            }

            return View(resetPasswordViewModel);
        }

        return RedirectToAction(nameof(ResetPasswordResult));
    }
    
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Clear();
        await _authService.LogoutAsync();
        return RedirectToAction(nameof(Login));
    }
    
    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);
    
        return RedirectToAction(nameof(Index), "Home");
    }
}