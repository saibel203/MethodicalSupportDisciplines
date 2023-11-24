using AspNetCoreHero.ToastNotification.Extensions;
using MethodicalSupportDisciplines.Infrastructure;
using MethodicalSupportDisciplines.MVC;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager applicationConfiguration = builder.Configuration;

builder.Services.AddInfrastructureServices(applicationConfiguration);
builder.Services.AddBasicsWebServices(applicationConfiguration);

WebApplication app = builder.Build();
/*IWebHostEnvironment environment = app.Environment;

if (environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    SeedDataDbContext initDataContextSeed = scope.ServiceProvider.GetRequiredService<SeedDataDbContext>();
    await initDataContextSeed.InitializeDatabaseAsync();
    await initDataContextSeed.SeedContextDataAsync();
}*/

app.UseNotyf();
app.UseHttpsRedirection();
app.UseHsts();

app.UseStaticFiles();
app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
