using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.BLL.Interfaces.Additional;
using MethodicalSupportDisciplines.Shared.AdditionalModels;
using MethodicalSupportDisciplines.Shared.Constants;
using MethodicalSupportDisciplines.Shared.Dto.Users;
using MethodicalSupportDisciplines.Shared.Responses.Services;
using MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;
using MethodicalSupportDisciplines.Shared.ViewModels.Forms.AssignRoles;
using MethodicalSupportDisciplines.Shared.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace MethodicalSupportDisciplines.MVC.Controllers;

[Authorize(Roles = ApplicationRoles.AdminRole)]
public class AdminController : BaseController
{
    private readonly IUsersService _usersService;
    private readonly IAuthService _authService;
    private readonly IQualificationService _qualificationService;
    private readonly IFacultyService _facultyService;
    private readonly IFormatLearningService _formatLearningService;
    private readonly IGroupService _groupService;
    private readonly ISpecialityService _specialityService;
    private readonly ILearningStatusService _learningStatusService;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<AdminController> _stringLocalization;

    public AdminController(IUsersService usersService, INotificationService notificationService,
        IQualificationService qualificationService, IFacultyService facultyService,
        IFormatLearningService formatLearningService, IGroupService groupService, ISpecialityService specialityService,
        ILearningStatusService learningStatusService, IAuthService authService, IMapper mapper,
        IStringLocalizer<AdminController> stringLocalization) : base(
        notificationService)
    {
        _usersService = usersService;
        _qualificationService = qualificationService;
        _facultyService = facultyService;
        _formatLearningService = formatLearningService;
        _groupService = groupService;
        _specialityService = specialityService;
        _learningStatusService = learningStatusService;
        _authService = authService;
        _mapper = mapper;
        _stringLocalization = stringLocalization;
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

    [HttpGet]
    public async Task<IActionResult> AssignTeacherRole(int userId)
    {
        CreateTeacherViewModel viewModel = await InitCreateTeacherViewModel();
        UsersServiceResponse getUserResponse = await _usersService.GetGuestUserByIdAsync(userId);

        if (!getUserResponse.IsSuccess || getUserResponse.GuestUser is null)
        {
            NotificationService.CustomErrorMessage(getUserResponse.Message);
            return RedirectToAction(nameof(GuestUsers));
        }

        viewModel.GuestUserId = getUserResponse.GuestUser.GuestUserId;
        viewModel.FirstName = getUserResponse.GuestUser.FirstName;
        viewModel.LastName = getUserResponse.GuestUser.LastName;
        viewModel.Patronymic = getUserResponse.GuestUser.Patronymic;
        viewModel.ApplicationUserId = getUserResponse.GuestUser.ApplicationUserId;

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AssignTeacherRole(CreateTeacherViewModel viewModel)
    {
        viewModel = await InitCreateTeacherViewModel(viewModel);

        if (!ModelState.IsValid)
        {
            NotificationService.CustomErrorMessage(_stringLocalization["AssignTeacherRoleError"]);
            return View(viewModel);
        }

        CreateTeacherDto createTeacherDto = _mapper.Map<CreateTeacherDto>(viewModel);

        UsersServiceResponse removeUserResponse =
            await _usersService.RemoveGuestUserWithoutApplicationUserAsync(viewModel.GuestUserId);

        if (!removeUserResponse.IsSuccess)
        {
            NotificationService.CustomErrorMessage(removeUserResponse.Message);
            return RedirectToAction(nameof(GuestUsers));
        }
        
        UserAuthResponse assignTeacherRoleResponse =
            await _authService.AssignTeacherRoleAsync(createTeacherDto);

        if (!assignTeacherRoleResponse.IsSuccess)
        {
            NotificationService.CustomErrorMessage(assignTeacherRoleResponse.Message);
            return RedirectToAction(nameof(GuestUsers));
        }

        NotificationService.CustomSuccessMessage(_stringLocalization["AssignTeacherSuccess"]);

        return RedirectToAction(nameof(GuestUsers));
    }

    [HttpGet]
    public async Task<IActionResult> AssignStudentRole(int userId)
    {
        CreateStudentViewModel viewModel = await InitCreateStudentViewModel();
        UsersServiceResponse getUserResponse = await _usersService.GetGuestUserByIdAsync(userId);

        if (!getUserResponse.IsSuccess || getUserResponse.GuestUser is null)
        {
            NotificationService.CustomErrorMessage(getUserResponse.Message);
            return RedirectToAction(nameof(GuestUsers));
        }
        
        viewModel.GuestUserId = getUserResponse.GuestUser.GuestUserId;
        viewModel.FirstName = getUserResponse.GuestUser.FirstName;
        viewModel.LastName = getUserResponse.GuestUser.LastName;
        viewModel.Patronymic = getUserResponse.GuestUser.Patronymic;
        viewModel.ApplicationUserId = getUserResponse.GuestUser.ApplicationUserId;
        
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AssignStudentRole(CreateStudentViewModel viewModel)
    {
        viewModel = await InitCreateStudentViewModel(viewModel);

        if (!ModelState.IsValid)
        {
            NotificationService.CustomErrorMessage(_stringLocalization["AssignTeacherRoleError"]);
            return View(viewModel);
        }

        CreateStudentDto createStudentDto = _mapper.Map<CreateStudentDto>(viewModel);
        
        UsersServiceResponse removeUserResponse =
            await _usersService.RemoveGuestUserWithoutApplicationUserAsync(viewModel.GuestUserId);

        if (!removeUserResponse.IsSuccess)
        {
            NotificationService.CustomErrorMessage(removeUserResponse.Message);
            return RedirectToAction(nameof(GuestUsers));
        }
        
        UserAuthResponse assignStudentRoleResponse =
            await _authService.AssignStudentRoleAsync(createStudentDto);

        if (!assignStudentRoleResponse.IsSuccess)
        {
            NotificationService.CustomErrorMessage(assignStudentRoleResponse.Message);
            return RedirectToAction(nameof(GuestUsers));
        }

        NotificationService.CustomSuccessMessage(_stringLocalization["AssignStudentSuccess"]);

        return RedirectToAction(nameof(GuestUsers));
    }

    private async Task<CreateTeacherViewModel> InitCreateTeacherViewModel(CreateTeacherViewModel? viewModel = null)
    {
        QualificationServiceResponse qualificationResponse = await _qualificationService.GetQualificationsAsync();

        if (!qualificationResponse.IsSuccess)
        {
            NotificationService.CustomErrorMessage("");
            return new CreateTeacherViewModel();
        }

        if (viewModel is null)
        {
            CreateTeacherViewModel createTeacherViewModel = new CreateTeacherViewModel
            {
                Qualifications = qualificationResponse.Qualifications
            };

            return createTeacherViewModel;
        }

        viewModel.Qualifications = qualificationResponse.Qualifications;

        return viewModel;
    }

    private async Task<CreateStudentViewModel> InitCreateStudentViewModel(CreateStudentViewModel? viewModel = null)
    {
        FacultyServiceResponse facultyResponse =
            await _facultyService.GetAllFacultiesAsync();
        FormatLearningServiceResponse formatLearningResponse =
            await _formatLearningService.GetFormatLearningsAsync();
        GroupServiceResponse groupResponse =
            await _groupService.GetAllGroupsAsync();
        LearningStatusServiceResponse learningStatusResponse =
            await _learningStatusService.GetAllLearningStatusesAsync();
        SpecialityServiceResponse specialityResponse =
            await _specialityService.GetAllSpecialitiesAsync();

        if (!facultyResponse.IsSuccess)
        {
            NotificationService.CustomErrorMessage("");
            return new CreateStudentViewModel();
        }

        if (!formatLearningResponse.IsSuccess)
        {
            NotificationService.CustomErrorMessage("");
            return new CreateStudentViewModel();
        }

        if (!groupResponse.IsSuccess)
        {
            NotificationService.CustomErrorMessage("");
            return new CreateStudentViewModel();
        }

        if (!learningStatusResponse.IsSuccess)
        {
            NotificationService.CustomErrorMessage("");
            return new CreateStudentViewModel();
        }

        if (!specialityResponse.IsSuccess)
        {
            NotificationService.CustomErrorMessage("");
            return new CreateStudentViewModel();
        }

        if (viewModel is null)
        {
            CreateStudentViewModel createStudentViewModel = new CreateStudentViewModel
            {
                Faculties = facultyResponse.Faculties,
                FormatLearnings = formatLearningResponse.FormatLearnings,
                Groups = groupResponse.Groups,
                LearningStatuses = learningStatusResponse.LearningStatuses,
                Specialities = specialityResponse.Specialties
            };

            return createStudentViewModel;
        }

        viewModel.Faculties = facultyResponse.Faculties;
        viewModel.LearningStatuses = learningStatusResponse.LearningStatuses;
        viewModel.FormatLearnings = formatLearningResponse.FormatLearnings;
        viewModel.Groups = groupResponse.Groups;
        viewModel.Specialities = specialityResponse.Specialties;

        return viewModel;
    }
}