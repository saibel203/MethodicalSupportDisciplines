using AspNetCoreHero.ToastNotification.Extensions;
using MethodicalSupportDisciplines.Infrastructure;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext.Seeds;
using MethodicalSupportDisciplines.MVC;
using MethodicalSupportDisciplines.MVC.Extensions;
using Microsoft.Extensions.Options;

/*                                                  TODOOOOOOOOOOOOOOO
 *  
 *
 *
 *
 *
 *
 *
 *
 * 
 */









WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager applicationConfiguration = builder.Configuration;

builder.Services.AddInfrastructureServices(applicationConfiguration);
builder.Services.AddBasicsWebServices(applicationConfiguration);

WebApplication app = builder.Build();
RequestLocalizationOptions localizationOptions = app.Services
    .GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;

IWebHostEnvironment environment = app.Environment;

if (environment.IsDevelopment())
{
    using IServiceScope scope = app.Services.CreateScope();

    SeedDataDbContext initDataContextSeed = scope.ServiceProvider.GetRequiredService<SeedDataDbContext>();
    await initDataContextSeed.InitializeDatabaseAsync();
    await initDataContextSeed.SeedContextDataAsync();
}

app.UsePageErrors();

app.UseNotyf();
app.UseHttpsRedirection();
app.UseHsts();

app.UseStaticFiles();
app.UseRouting();

app.UseRequestLocalization(localizationOptions);

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
