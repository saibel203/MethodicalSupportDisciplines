using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MethodicalSupportDisciplines.MVC.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class UnauthorizedAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity?.IsAuthenticated == true)
            context.Result = new RedirectResult("/Home/Index");
    }
}