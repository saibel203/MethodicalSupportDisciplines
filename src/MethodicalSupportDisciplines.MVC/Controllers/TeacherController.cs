using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.BLL.Interfaces.Additional;
using MethodicalSupportDisciplines.BLL.Interfaces.Learning;
using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.MVC.Controllers.Base;
using MethodicalSupportDisciplines.Shared.Constants;
using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.Enums;
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
    private readonly IDisciplineMaterialService _disciplineMaterialService;
    private readonly IDisciplineMaterialTypeService _disciplineMaterialTypeService;
    private readonly IDisciplineGroupService _disciplineGroupService;
    private readonly IGroupService _groupService;

    public TeacherController(INotificationService notificationService, IDisciplineService disciplineService,
        IMapper mapper, IUsersService usersService, IStringLocalizer<TeacherController> stringLocalization,
        IMaterialTypeService materialTypeService, IMaterialService materialService,
        IMaterialDisciplineMaterialService materialDisciplineMaterialService, IFileService fileService,
        IDisciplineMaterialService disciplineMaterialService,
        IDisciplineMaterialTypeService disciplineMaterialTypeService,
        IDisciplineGroupService disciplineGroupService, IGroupService groupService) : base(
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
        _disciplineMaterialService = disciplineMaterialService;
        _disciplineMaterialTypeService = disciplineMaterialTypeService;
        _disciplineGroupService = disciplineGroupService;
        _groupService = groupService;
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

        DisciplineMaterialTypeServiceResponse disciplineMaterialTypeResponse =
            await _disciplineMaterialTypeService.GetDisciplineMaterialTypesAsync();

        if (!disciplineMaterialTypeResponse.IsSuccess)
        {
            NotificationService.CustomErrorMessage(disciplineMaterialTypeResponse.Message);
            return RedirectToAction(nameof(Disciplines));
        }

        GroupServiceResponse getGroupsResponse = await _groupService.GetAllGroupsAsync();
        
        if (!getGroupsResponse.IsSuccess)
        {
            NotificationService.CustomErrorMessage(getGroupsResponse.Message);
            return RedirectToAction(nameof(Disciplines));
        }

        DisciplinesViewModel viewModel = _mapper.Map<DisciplinesViewModel>(getDisciplineResult);
        viewModel.MaterialTypes = materialTypesResult.MaterialTypes;
        viewModel.DisciplineMaterialTypes = disciplineMaterialTypeResponse.DisciplineMaterials;
        viewModel.Groups = getGroupsResponse.Groups;

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDisciplineMaterialMaterial(DisciplinesViewModel viewModel)
    {
        NewMaterialDto newMaterialDto =
            _mapper.Map<NewMaterialDto>(viewModel.CreateDisciplineMaterialSubMaterialViewModel);

        int createMaterialId;

        if (viewModel.CreateDisciplineMaterialSubMaterialViewModel.FormFile is null &&
            viewModel.CreateDisciplineMaterialSubMaterialViewModel.MaterialTypeId == (int)MaterialTypes.Href)
        {
            MaterialServiceResponse createMaterialResult =
                await _materialService.CreateMaterialAsync(newMaterialDto);

            if (!createMaterialResult.IsSuccess)
            {
                NotificationService.CustomErrorMessage(createMaterialResult.Message);
                return RedirectToAction(nameof(DisciplineData),
                    new
                        { disciplineId = viewModel.CreateDisciplineMaterialSubMaterialViewModel.DisciplineId });
            }

            createMaterialId = createMaterialResult.CreatedMaterialId;
        }
        else
        {
            FileResponse fileResponse =
                await _fileService.UploadMaterialFileAsync(
                    viewModel.CreateDisciplineMaterialSubMaterialViewModel.FormFile!,
                    viewModel.CreateDisciplineMaterialSubMaterialViewModel.MaterialTypeId);

            createMaterialId = fileResponse.CreateMaterialId;
        }

        NewMaterialDisciplineMaterialDto newRelationship = new NewMaterialDisciplineMaterialDto
        {
            MaterialId = createMaterialId,
            DisciplineMaterialId = viewModel.CreateDisciplineMaterialSubMaterialViewModel.DisciplineMaterialId
        };

        MaterialDisciplineMaterialServiceResponse newMaterialDisciplineMaterialResult =
            await _materialDisciplineMaterialService.CreateMaterialDisciplineMaterialAsync(newRelationship);

        if (!newMaterialDisciplineMaterialResult.IsSuccess)
        {
            NotificationService.CustomErrorMessage(newMaterialDisciplineMaterialResult.Message);
            return RedirectToAction(nameof(DisciplineData),
                new
                    { disciplineId = viewModel.CreateDisciplineMaterialSubMaterialViewModel.DisciplineId });
        }

        NotificationService.CustomSuccessMessage(_stringLocalization["SuccessUploadMaterial"]);
        return RedirectToAction(nameof(DisciplineData),
            new
                { disciplineId = viewModel.CreateDisciplineMaterialSubMaterialViewModel.DisciplineId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDiscipline(DisciplinesViewModel viewModel)
    {
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

    [HttpGet]
    public async Task<IActionResult> RemoveDisciplineMaterialMaterial(int materialId, int disciplineId)
    {
        MaterialServiceResponse removeResult = await _materialService.RemoveMaterialAsync(materialId);

        if (!removeResult.IsSuccess)
        {
            NotificationService.CustomErrorMessage(removeResult.Message);
            return RedirectToAction(nameof(DisciplineData), new { disciplineId });
        }

        if (removeResult.Material.MaterialTypeId == (int)MaterialTypes.Book ||
            removeResult.Material.MaterialTypeId == (int)MaterialTypes.File)
        {
            FileResponse removeFileResult =
                _fileService.RemoveMaterialFileAsync(removeResult.Material.MaterialPath);

            if (!removeResult.IsSuccess)
            {
                NotificationService.CustomErrorMessage(removeFileResult.Message);
                return RedirectToAction(nameof(DisciplineData), new { disciplineId });
            }
        }

        NotificationService.CustomSuccessMessage(removeResult.Message);
        return RedirectToAction(nameof(DisciplineData), new { disciplineId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDisciplineMaterial(DisciplinesViewModel viewModel)
    {
        NewDisciplineMaterialDto createdDto =
            _mapper.Map<NewDisciplineMaterialDto>(viewModel.CreateDisciplineMaterialViewModel);

        DisciplineMaterialServiceResponse createResponse =
            await _disciplineMaterialService.CreateDisciplineMaterialAsync(createdDto);

        if (!createResponse.IsSuccess)
        {
            NotificationService.CustomErrorMessage(createResponse.Message);
            return RedirectToAction(nameof(DisciplineData),
                new { disciplineId = viewModel.CreateDisciplineMaterialViewModel.DisciplineId });
        }

        NotificationService.CustomSuccessMessage(createResponse.Message);
        return RedirectToAction(nameof(DisciplineData),
            new { disciplineId = viewModel.CreateDisciplineMaterialViewModel.DisciplineId });
    }

    [HttpGet]
    public async Task<IActionResult> RemoveDisciplineMaterial(int disciplineMaterialId, int disciplineId)
    {
        DisciplineMaterialServiceResponse removeResult =
            await _disciplineMaterialService.RemoveDisciplineMaterialAsync(disciplineMaterialId);

        if (!removeResult.IsSuccess)
        {
            NotificationService.CustomErrorMessage(removeResult.Message);
            return RedirectToAction(nameof(DisciplineData), new { disciplineId });
        }

        foreach (MaterialDisciplineMaterial material in removeResult.DisciplineMaterial.Materials)
        {
            MaterialServiceResponse removeResultMaterial =
                await _materialService.RemoveMaterialAsync(material.Material.MaterialId);

            if (!removeResultMaterial.IsSuccess)
            {
                NotificationService.CustomErrorMessage(removeResult.Message);
                return RedirectToAction(nameof(DisciplineData), new { disciplineId });
            }

            if (removeResultMaterial.Material.MaterialTypeId == (int)MaterialTypes.Book ||
                removeResultMaterial.Material.MaterialTypeId == (int)MaterialTypes.File)
            {
                FileResponse removeFileResult =
                    _fileService.RemoveMaterialFileAsync(removeResultMaterial.Material.MaterialPath);

                if (!removeResult.IsSuccess)
                {
                    NotificationService.CustomErrorMessage(removeFileResult.Message);
                    return RedirectToAction(nameof(DisciplineData), new { disciplineId });
                }
            }
        }

        NotificationService.CustomSuccessMessage(removeResult.Message);
        return RedirectToAction(nameof(DisciplineData), new { disciplineId });
    }

    [HttpGet]
    public async Task<IActionResult> RemoveDisciplineGroup(int disciplineId, int groupId)
    {
        DisciplineGroupServiceResponse removeResponse = 
            await _disciplineGroupService.RemoveDisciplineGroupAsync(disciplineId, groupId);

        if (!removeResponse.IsSuccess)
        {
            NotificationService.CustomErrorMessage(removeResponse.Message);
            return RedirectToAction(nameof(DisciplineData), new { disciplineId });
        }
        
        NotificationService.CustomSuccessMessage(removeResponse.Message);
        return RedirectToAction(nameof(DisciplineData), new { disciplineId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDisciplineGroup(DisciplinesViewModel viewModel)
    {
        NewDisciplineGroupDto createDto = 
            _mapper.Map<NewDisciplineGroupDto>(viewModel.CreateDisciplineGroupViewModel);

        DisciplineGroupServiceResponse createResponse = 
            await _disciplineGroupService.CreateDisciplineGroupAsync(createDto);

        if (!createResponse.IsSuccess)
        {
            NotificationService.CustomErrorMessage(createResponse.Message);
            return RedirectToAction(nameof(DisciplineData), 
                new { disciplineId = viewModel.CreateDisciplineGroupViewModel.DisciplineId });
        }
        
        NotificationService.CustomSuccessMessage(createResponse.Message);
        return RedirectToAction(nameof(DisciplineData), 
            new { disciplineId = viewModel.CreateDisciplineGroupViewModel.DisciplineId });
    }
    
    public async Task<IActionResult> Account()
    {
        string userId = GetUserId();

        UsersServiceResponse response = await _usersService.GetTeacherUserAccountAsync(userId);

        if (!response.IsSuccess)
        {
            NotificationService.CustomErrorMessage(response.Message);
            return RedirectToAction(nameof(Index), "Home");
        }

        return View(response.TeacherUser);
    }
}