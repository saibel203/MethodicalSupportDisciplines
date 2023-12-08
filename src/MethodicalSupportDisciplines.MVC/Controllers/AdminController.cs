using MethodicalSupportDisciplines.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MethodicalSupportDisciplines.MVC.Controllers;

[Authorize(Roles = ApplicationRoles.AdminRole)]
public class AdminController : Controller
{
    [HttpGet]
    public IActionResult GuestUsers()
    {
        return View();
    }
}