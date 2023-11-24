using Microsoft.AspNetCore.Mvc;

namespace MethodicalSupportDisciplines.MVC.Controllers;

public class AuthController : Controller
{
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
}