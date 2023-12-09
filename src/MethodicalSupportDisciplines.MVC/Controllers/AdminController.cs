using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.Shared.AdditionalModels;
using MethodicalSupportDisciplines.Shared.Constants;
using MethodicalSupportDisciplines.Shared.Responses.Services;
using MethodicalSupportDisciplines.Shared.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MethodicalSupportDisciplines.MVC.Controllers;

[Authorize(Roles = ApplicationRoles.AdminRole)]
public class AdminController : BaseController
{
    private readonly IUsersService _usersService;

    public AdminController(IUsersService usersService, INotificationService notificationService) : base(
        notificationService)
    {
        _usersService = usersService;
    }

    [HttpGet]
    public async Task<IActionResult> GuestUsers(QueryParameters queryParameters)
    {
        UsersServiceResponse resultUsers = await _usersService.GetGuestUsersAsync(queryParameters);

        string username = GetUsername();

        if (!resultUsers.IsSuccess || resultUsers.GuestUsers is null)
        {
            return NotFound();
        }

        AdminGuestUsersViewModel viewModel = new()
        {
            GuestUsers = resultUsers.GuestUsers,
            Username = username,
            Pages = resultUsers.Pages,
            ItemsCount = resultUsers.ItemsCount,
            PageCount = resultUsers.PageCount,
            QueryParameters = queryParameters,
            CurrentController = GetCurrentControllerName(),
            CurrentAction = GetCurrentActionName()
        };

        IActionResult actionResult = CheckQueryParametersForPageBaseCondition(
            ref queryParameters, viewModel, resultUsers);

        return actionResult;
    }

    [HttpGet]
    public async Task<IActionResult> RemoveGuestUser(int userId)
    {
        UsersServiceResponse removeResult = await _usersService.RemoveGuestUserAsync(userId);

        if (!removeResult.IsSuccess)
        {
            NotificationService.CustomErrorMessage(removeResult.Message);
            return RedirectToAction(nameof(GuestUsers));
        }

        NotificationService.CustomSuccessMessage(removeResult.Message);
        return RedirectToAction(nameof(GuestUsers));
    }

    [HttpGet]
    public async Task<IActionResult> AssignRole(int userId)
    {
        UsersServiceResponse getUserResult = await _usersService.GetGuestUserByIdAsync(userId);

        if (!getUserResult.IsSuccess)
        {
            NotificationService.CustomErrorMessage(getUserResult.Message);
            return RedirectToAction(nameof(GuestUsers));
        }

        return View(getUserResult.GuestUser);
    }
}