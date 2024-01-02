namespace MethodicalSupportDisciplines.MVC.Middlewares;

public class PageErrorsMiddleware(ILogger<PageErrorsMiddleware> logger, RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);

            if (context.Response.StatusCode == 404)
            {
                const string pagePath = "/Error/PageNotFound";
                
                context.Request.Path = pagePath;
                await next(context);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Page retrieval error");
        }
    }
}