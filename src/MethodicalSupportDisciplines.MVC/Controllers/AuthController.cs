using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.Shared.Dto;
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
    public async Task<IActionResult> ConfirmEmailResult(string userId, string token)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
        {
            return RedirectToAction(nameof(Login));
        }
        
        UserAuthResponse result = await _authService.ConfirmEmailAsync(userId, token);
        
        if (!result.IsSuccess)
        {
            if (result.Errors is not null)
            {
                foreach (var error in result.Errors)
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