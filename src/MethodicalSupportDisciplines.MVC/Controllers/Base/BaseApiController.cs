using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace MethodicalSupportDisciplines.MVC.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    private protected string GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
}