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
            return View(resultUsers.Message);
        }

        AdminGuestUsersViewModel viewModel = new AdminGuestUsersViewModel
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

    [HttpGet]
    public async Task<IActionResult> TeacherUsers(QueryParameters queryParameters)
    {
        UsersServiceResponse getUsersResult = await _usersService.GetTeacherUsersAsync(queryParameters);

        string username = GetUsername();

        if (!getUsersResult.IsSuccess || getUsersResult.TeacherUsers is null)
        {
            return View(getUsersResult.Message);
        }

        AdminTeacherUsersViewModel viewModel = new AdminTeacherUsersViewModel
        {
            TeacherUsers = getUsersResult.TeacherUsers,
            Username = username,
            Pages = getUsersResult.Pages,
            ItemsCount = getUsersResult.ItemsCount,
            PageCount = getUsersResult.PageCount,
            QueryParameters = queryParameters,
            CurrentController = GetCurrentControllerName(),
            CurrentAction = GetCurrentActionName()
        };

        IActionResult actionResult = CheckQueryParametersForPageBaseCondition(
            ref queryParameters, viewModel, getUsersResult);

        return actionResult;
    }
    
    [HttpGet]
    public async Task<IActionResult> RemoveTeacherUser(int userId)
    {
        UsersServiceResponse removeResult = await _usersService.RemoveTeacherUserAsync(userId);

        if (!removeResult.IsSuccess)
        {
            NotificationService.CustomErrorMessage(removeResult.Message);
            return RedirectToAction(nameof(TeacherUsers));
        }

        NotificationService.CustomSuccessMessage(removeResult.Message);
        return RedirectToAction(nameof(TeacherUsers));
    }

    [HttpGet]
    public async Task<IActionResult> StudentUsers(QueryParameters queryParameters)
    {
        UsersServiceResponse getUsersResult = await _usersService.GetStudentUsersAsync(queryParameters);

        string username = GetUsername();

        if (!getUsersResult.IsSuccess || getUsersResult.StudentUsers is null)
        {
            return View(getUsersResult.Message);
        }

        AdminStudentUsersViewModel viewModel = new AdminStudentUsersViewModel
        {
            StudentUsers = getUsersResult.StudentUsers,
            Username = username,
            Pages = getUsersResult.Pages,
            ItemsCount = getUsersResult.ItemsCount,
            PageCount = getUsersResult.PageCount,
            QueryParameters = queryParameters,
            CurrentController = GetCurrentControllerName(),
            CurrentAction = GetCurrentActionName()
        };

        IActionResult actionResult = CheckQueryParametersForPageBaseCondition(
            ref queryParameters, viewModel, getUsersResult);

        return actionResult;
    }
    
    [HttpGet]
    public async Task<IActionResult> RemoveStudentUser(int userId)
    {
        UsersServiceResponse removeResult = await _usersService.RemoveStudentUserAsync(userId);

        if (!removeResult.IsSuccess)
        {
            NotificationService.CustomErrorMessage(removeResult.Message);
            return RedirectToAction(nameof(StudentUsers));
        }

        NotificationService.CustomSuccessMessage(removeResult.Message);
        return RedirectToAction(nameof(StudentUsers));
    }
}