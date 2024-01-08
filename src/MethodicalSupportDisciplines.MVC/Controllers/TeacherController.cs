using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.BLL.Interfaces.Additional;
using MethodicalSupportDisciplines.BLL.Interfaces.Learning;
using MethodicalSupportDisciplines.MVC.Controllers.Base;
using MethodicalSupportDisciplines.Shared.Constants;
using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Services;
using MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;
using MethodicalSupportDisciplines.Shared.Responses.Services.LearningServicesResponses;
using MethodicalSupportDisciplines.Shared.ViewModels.Forms.Learning;
using MethodicalSupportDisciplines.Shared.ViewModels.Learning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace MethodicalSupportDisciplines.MVC.Controllers;

[Authorize(Roles = ApplicationRoles.TeacherRole)]
public class TeacherController : BaseController
{
    private readonly IDisciplineService _disciplineService;
    private readonly IUsersService _usersService;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<TeacherController> _stringLocalization;
    private readonly IMaterialTypeService _materialTypeService;
    private readonly IMaterialService _materialService;
    private readonly IMaterialDisciplineMaterialService _materialDisciplineMaterialService;
    private readonly IFileService _fileService;

    public TeacherController(INotificationService notificationService, IDisciplineService disciplineService,
        IMapper mapper, IUsersService usersService, IStringLocalizer<TeacherController> stringLocalization,
        IMaterialTypeService materialTypeService, IMaterialService materialService,
        IMaterialDisciplineMaterialService materialDisciplineMaterialService, IFileService fileService) : base(
        notificationService)
    {
        _disciplineService = disciplineService;
        _mapper = mapper;
        _usersService = usersService;
        _stringLocalization = stringLocalization;
        _materialTypeService = materialTypeService;
        _materialService = materialService;
        _materialDisciplineMaterialService = materialDisciplineMaterialService;
        _fileService = fileService;
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

        UsersServiceResponse userData =
            await _usersService.GetTeacherUserByApplicationUserIdAsync(currentUserId);

        ViewData["CurrentTeacherId"] = userData.TeacherUserId;

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

        MaterialTypeServiceResponse materialTypesResult =
            await _materialTypeService.GetMaterialTypesASync();

        if (!materialTypesResult.IsSuccess)
        {
            NotificationService.CustomErrorMessage(materialTypesResult.Message);
            return RedirectToAction(nameof(Disciplines));
        }

        DisciplinesViewModel viewModel = _mapper.Map<DisciplinesViewModel>(getDisciplineResult);
        viewModel.MaterialTypes = materialTypesResult.MaterialTypes;

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDisciplineMaterial(DisciplinesViewModel viewModel)
    {
        NewMaterialDto newMaterialDto =
            _mapper.Map<NewMaterialDto>(viewModel.CreateDisciplineMaterialViewModel);

        int createMaterialId;

        if (viewModel.CreateDisciplineMaterialViewModel.MaterialTypeId == 1
            && viewModel.CreateDisciplineMaterialViewModel.FormFile is null)
        {
            MaterialServiceResponse createMaterialResult =
                await _materialService.CreateMaterialAsync(newMaterialDto);

            if (!createMaterialResult.IsSuccess)
            {
                NotificationService.CustomErrorMessage(createMaterialResult.Message);
                return RedirectToAction(nameof(DisciplineData),
                    new
                        { disciplineId = viewModel.CreateDisciplineMaterialViewModel.DisciplineId });
            }

            createMaterialId = createMaterialResult.CreatedMaterialId;
        }
        else
        {
            FileResponse fileResponse = 
                await _fileService.UploadMaterialFileAsync(
                    viewModel.CreateDisciplineMaterialViewModel.FormFile!, 
                    viewModel.CreateDisciplineMaterialViewModel.MaterialTypeId);

            createMaterialId = fileResponse.CreateMaterialId;
        }

        /*MaterialServiceResponse createMaterialResult =
            await _materialService.CreateMaterialAsync(newMaterialDto);

        if (!createMaterialResult.IsSuccess)
        {
            NotificationService.CustomErrorMessage(createMaterialResult.Message);
            return RedirectToAction(nameof(DisciplineData),
                new
                    { disciplineId = viewModel.CreateDisciplineMaterialViewModel.DisciplineId });
        }*/

        NewMaterialDisciplineMaterialDto newRelationship = new NewMaterialDisciplineMaterialDto
        {
            MaterialId = createMaterialId,
            DisciplineMaterialId = viewModel.CreateDisciplineMaterialViewModel.DisciplineMaterialId
        };

        MaterialDisciplineMaterialServiceResponse newMaterialDisciplineMaterialResult =
            await _materialDisciplineMaterialService.CreateMaterialDisciplineMaterialAsync(newRelationship);

        if (!newMaterialDisciplineMaterialResult.IsSuccess)
        {
            NotificationService.CustomErrorMessage(newMaterialDisciplineMaterialResult.Message);
            return RedirectToAction(nameof(DisciplineData),
                new
                    { disciplineId = viewModel.CreateDisciplineMaterialViewModel.DisciplineId });
        }

        NotificationService.CustomSuccessMessage("SUCCESS");
        return RedirectToAction(nameof(DisciplineData),
            new
                { disciplineId = viewModel.CreateDisciplineMaterialViewModel.DisciplineId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDiscipline(DisciplinesViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            NotificationService.CustomErrorMessage(_stringLocalization["CreateDisciplineError"]);
            return RedirectToAction(nameof(Disciplines), viewModel);
        }

        NewDisciplineDto dto = _mapper.Map<NewDisciplineDto>(viewModel.CreateDisciplineViewModel);
        DisciplineServiceResponse createDisciplineResult =
            await _disciplineService.CreateDisciplineAsync(dto);

        if (!createDisciplineResult.IsSuccess)
        {
            NotificationService.CustomErrorMessage(createDisciplineResult.Message);
            return RedirectToAction(nameof(Disciplines), viewModel);
        }

        NotificationService.CustomSuccessMessage(createDisciplineResult.Message);
        return RedirectToAction(nameof(Disciplines));
    }

    [HttpGet]
    public async Task<IActionResult> RemoveDiscipline(int disciplineId)
    {
        DisciplineServiceResponse removeResult =
            await _disciplineService.RemoveDisciplineAsync(disciplineId);

        if (!removeResult.IsSuccess)
        {
            NotificationService.CustomErrorMessage(removeResult.Message);
            return RedirectToAction(nameof(Disciplines));
        }

        NotificationService.CustomSuccessMessage(removeResult.Message);
        return RedirectToAction(nameof(Disciplines));
    }
}