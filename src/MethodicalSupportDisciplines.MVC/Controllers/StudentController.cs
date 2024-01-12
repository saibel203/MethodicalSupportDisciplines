using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.MVC.Controllers.Base;
using MethodicalSupportDisciplines.Shared.Constants;
using Microsoft.AspNetCore.Authorization;

namespace MethodicalSupportDisciplines.MVC.Controllers;

[Authorize(Roles = ApplicationRoles.StudentRole)]
public class StudentController : BaseController
{
    public StudentController(INotificationService notificationService) : base(
        notificationService)
    {
    }
}