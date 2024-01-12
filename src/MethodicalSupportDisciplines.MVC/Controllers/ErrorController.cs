using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.MVC.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace MethodicalSupportDisciplines.MVC.Controllers;

public class ErrorController(INotificationService notificationService) : BaseController(notificationService)
{
    [HttpGet]
    public IActionResult PageNotFound() => View();
    
    public IActionResult AccessDenied() => View();
}