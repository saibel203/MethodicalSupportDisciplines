using MethodicalSupportDisciplines.MVC.Middlewares;

namespace MethodicalSupportDisciplines.MVC.Extensions;

public static class PageErrorsMiddlewareExtension
{
    public static void UsePageErrors(this IApplicationBuilder app)
    {
        app.UseMiddleware<PageErrorsMiddleware>();
    }
}