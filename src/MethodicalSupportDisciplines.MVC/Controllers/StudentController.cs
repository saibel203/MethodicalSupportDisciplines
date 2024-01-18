using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.BLL.Interfaces.Learning;
using MethodicalSupportDisciplines.MVC.Controllers.Base;
using MethodicalSupportDisciplines.Shared.AdditionalModels;
using MethodicalSupportDisciplines.Shared.Constants;
using MethodicalSupportDisciplines.Shared.Responses.Services;
using MethodicalSupportDisciplines.Shared.Responses.Services.LearningServicesResponses;
using MethodicalSupportDisciplines.Shared.ViewModels.Learning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MethodicalSupportDisciplines.MVC.Controllers;

[Authorize(Roles = ApplicationRoles.StudentRole)]
public class StudentController : BaseController
{
    private readonly IDisciplineService _disciplineService;
    private readonly IUsersService _usersService;

    public StudentController(INotificationService notificationService, IDisciplineService disciplineService,
        IUsersService usersService) :
        base(
            notificationService)
    {
        _disciplineService = disciplineService;
        _usersService = usersService;
    }

    public async Task<IActionResult> Disciplines(QueryParameters queryParameters)
    {
        string currentUserId = GetUserId();
        string username = GetUsername();

        DisciplineServiceResponse getResult =
            await _disciplineService.GetDisciplinesForStudentAsync(currentUserId, queryParameters);

        if (!getResult.IsSuccess)
        {
            NotificationService.CustomErrorMessage(getResult.Message);
            return RedirectToAction(nameof(Index), "Home");
        }

        StudentDisciplinesViewModel viewModel = new StudentDisciplinesViewModel
        {
            DisciplineGroups = getResult.DisciplineGroups,
            Username = username,
            CurrentController = GetCurrentControllerName(),
            CurrentAction = GetCurrentActionName(),
            QueryParameters = queryParameters,
            Pages = getResult.Pages,
            ItemsCount = getResult.ItemsCount,
            PageCount = getResult.PageCount
        };

        IActionResult actionResult = CheckQueryParametersForPageBaseCondition(
            ref queryParameters, viewModel, getResult);

        return actionResult;
    }

    public async Task<IActionResult> DisciplineData(int disciplineId)
    {
        string currentUserId = GetUserId();

        DisciplineServiceResponse getDisciplineResult =
            await _disciplineService.GetDisciplineForStudentByIdAsync(disciplineId, currentUserId);

        if (!getDisciplineResult.IsSuccess)
        {
            NotificationService.CustomErrorMessage(getDisciplineResult.Message);
            return RedirectToAction(nameof(Disciplines));
        }

        StudentDisciplinesViewModel viewModel = new StudentDisciplinesViewModel
        {
            Discipline = getDisciplineResult.Discipline
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Account()
    {
        string userId = GetUserId();

        UsersServiceResponse response = await _usersService.GetStudentUserAccountAsync(userId);

        if (!response.IsSuccess)
        {
            NotificationService.CustomErrorMessage(response.Message);
            return RedirectToAction(nameof(Index), "Home");
        }

        return View(response.StudentUser);
    }
}