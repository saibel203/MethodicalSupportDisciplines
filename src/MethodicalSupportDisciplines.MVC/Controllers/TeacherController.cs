using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.BLL.Interfaces.Learning;
using MethodicalSupportDisciplines.MVC.Controllers.Base;
using MethodicalSupportDisciplines.Shared.Constants;
using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Services.LearningServicesResponses;
using MethodicalSupportDisciplines.Shared.ViewModels.Forms.Learning;
using MethodicalSupportDisciplines.Shared.ViewModels.Learning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MethodicalSupportDisciplines.MVC.Controllers;

[Authorize(Roles = ApplicationRoles.TeacherRole)]
public class TeacherController : BaseController
{
    private readonly IDisciplineService _disciplineService;
    private readonly IUsersService _usersService;
    private readonly IMapper _mapper;

    public TeacherController(INotificationService notificationService, IDisciplineService disciplineService,
        IMapper mapper, IUsersService usersService) : base(
        notificationService)
    {
        _disciplineService = disciplineService;
        _mapper = mapper;
        _usersService = usersService;
    }

    [HttpGet]
    public async Task<IActionResult> Disciplines(CreateDisciplineViewModel queryParameters)
    {
        string currentUserId = GetUserId();
        string username = GetUsername();
        DisciplineServiceResponse getDisciplinesResult = 
            await _disciplineService.GetAllDisciplinesAsync(queryParameters, currentUserId);

        if (!getDisciplinesResult.IsSuccess)
        {
            NotificationService.CustomErrorMessage(getDisciplinesResult.Message);
            return RedirectToAction(nameof(Index), "Home");
        }

        DisciplinesViewModel viewModel = new DisciplinesViewModel
        {
            Disciplines = getDisciplinesResult.Disciplines,
            Username = username,
            CurrentController = GetCurrentControllerName(),
            CurrentAction = GetCurrentActionName(),
            QueryParameters = queryParameters,
            Pages = getDisciplinesResult.Pages,
            ItemsCount = getDisciplinesResult.ItemsCount,
            PageCount = getDisciplinesResult.PageCount
        };

        var x = await _usersService.GetTeacherUserByApplicationUserIdAsync(currentUserId);
        
        ViewData["CurrentTeacherId"] = x.TeacherUserId; 
        
        IActionResult actionResult = CheckQueryParametersForPageBaseCondition(
            ref queryParameters, viewModel, getDisciplinesResult);

        return actionResult;
    }

    [HttpGet]
    public async Task<IActionResult> DisciplineData(int disciplineId)
    {
        string currentUserId = GetUserId();
        DisciplineServiceResponse getDisciplineResult =
            await _disciplineService.GetDisciplineByIdAsync(disciplineId, currentUserId);

        if (!getDisciplineResult.IsSuccess)
        {
            NotificationService.CustomErrorMessage(getDisciplineResult.Message);
            return RedirectToAction(nameof(Disciplines));
        }

        DisciplinesViewModel viewModel = _mapper.Map<DisciplinesViewModel>(getDisciplineResult);
        
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscipline(DisciplinesViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Disciplines), viewModel);
        }

        var dto = _mapper.Map<NewDisciplineDto>(viewModel);

        var test = await _disciplineService.CreateDisciplineAsync(dto);

        if (!test.IsSuccess)
        {
            NotificationService.CustomErrorMessage(test.Message);
            return RedirectToAction(nameof(Disciplines), viewModel);
        }

        return RedirectToAction(nameof(Disciplines));   
    }
}