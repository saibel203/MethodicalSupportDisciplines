using System.Security.Claims;
using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.Shared.AdditionalModels;
using MethodicalSupportDisciplines.Shared.Responses;
using MethodicalSupportDisciplines.Shared.ViewModels.Additional;
using MethodicalSupportDisciplines.Shared.ViewModels.Forms.Learning;
using Microsoft.AspNetCore.Mvc;

namespace MethodicalSupportDisciplines.MVC.Controllers.Base;

public class BaseController(INotificationService notificationService) : Controller
{
    private protected readonly INotificationService NotificationService = notificationService;

    private protected string GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

    private protected string GetUserRole() => User.FindFirst(ClaimTypes.Role)?.Value!;

    private protected string GetUsername() => User.Identity?.Name!;

    private protected string GetCurrentControllerName() =>
        ControllerContext.RouteData.Values["controller"]?.ToString()!;

    private protected string GetCurrentActionName() => ControllerContext.RouteData.Values["action"]?.ToString()!;

    private protected IActionResult CheckQueryParametersForPageBaseCondition<TService>(
        ref QueryParameters queryParameters, SettingsViewModel settingsViewModel, TService serviceResult)
        where TService : ListBaseResponse
    {
        string username = GetUsername();

        if (queryParameters.PageNumber < 1)
        {
            if (settingsViewModel.PageCount == 0)
            {
                NotificationService.CustomErrorMessage(
                    "Записів за вашим записом не знайдено або щось там");
                return View(settingsViewModel);
            }

            queryParameters.PageNumber = 1;
            return RedirectToAction(settingsViewModel.CurrentAction, new
            {
                queryParameters.PageNumber, queryParameters.SearchString, username
            });
        }

        if (queryParameters.PageNumber > serviceResult.PageCount)
        {
            queryParameters.PageNumber = serviceResult.PageCount;
            return RedirectToAction(settingsViewModel.CurrentAction, new
            {
                queryParameters.PageNumber, queryParameters.SearchString, username
            });
        }

        return View(settingsViewModel);
    }
    
    private protected IActionResult CheckQueryParametersForPageBaseCondition<TService>(
        ref CreateDisciplineViewModel queryParameters, SettingsViewModel settingsViewModel, TService serviceResult)
        where TService : ListBaseResponse
    {
        string username = GetUsername();

        if (queryParameters.PageNumber < 1)
        {
            if (settingsViewModel.PageCount == 0)
            {
                NotificationService.CustomErrorMessage(
                    "Записів за вашим записом не знайдено або щось там");
                return View(settingsViewModel);
            }

            queryParameters.PageNumber = 1;
            return RedirectToAction(settingsViewModel.CurrentAction, new
            {
                queryParameters.PageNumber, queryParameters.SearchString, username
            });
        }

        if (queryParameters.PageNumber > serviceResult.PageCount)
        {
            queryParameters.PageNumber = serviceResult.PageCount;
            return RedirectToAction(settingsViewModel.CurrentAction, new
            {
                queryParameters.PageNumber, queryParameters.SearchString, username
            });
        }

        return View(settingsViewModel);
    }
}