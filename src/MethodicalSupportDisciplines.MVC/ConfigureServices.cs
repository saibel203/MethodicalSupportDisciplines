using System.Globalization;
using AspNetCoreHero.ToastNotification;
using MethodicalSupportDisciplines.BLL.Infrastructure.ErrorDescribers;
using MethodicalSupportDisciplines.BLL.Infrastructure.MappingProfiles;
using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.BLL.Interfaces.Additional;
using MethodicalSupportDisciplines.BLL.Interfaces.Learning;
using MethodicalSupportDisciplines.Core.Models.Identity;
using MethodicalSupportDisciplines.BLL.Services;
using MethodicalSupportDisciplines.BLL.Services.Additional;
using MethodicalSupportDisciplines.BLL.Services.Learning;
using MethodicalSupportDisciplines.Core.IOptions;
using MethodicalSupportDisciplines.Data.Interfaces;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Data.Interfaces.Learning;
using MethodicalSupportDisciplines.Data.Repositories;
using MethodicalSupportDisciplines.Data.Repositories.Additional;
using MethodicalSupportDisciplines.Data.Repositories.Learning;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext.Seeds;
using MethodicalSupportDisciplines.Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

namespace MethodicalSupportDisciplines.MVC;

public static class ConfigureServices
{
    public static IServiceCollection AddBasicsWebServices(this IServiceCollection services, 
        IConfiguration configuration)
    {
        PowerShellTerminal powerShell = new PowerShellTerminal();
        powerShell.RunShellCommand("gulp watch-sass");
        powerShell.RunShellCommand("gulp watch-typescript");
        
        services.AddHttpContextAccessor();

        /* ------------------IDENTITY AUTHENTICATION SETTINGS------------------ */
        services.AddIdentity<ApplicationUser, ApplicationRole>(identityOptions =>
            {
                identityOptions.Password.RequiredLength = 6;
                identityOptions.Password.RequireLowercase = true;
                identityOptions.Password.RequireUppercase = true;
                identityOptions.Password.RequireDigit = true;
                identityOptions.Password.RequireNonAlphanumeric = false;

                identityOptions.User.RequireUniqueEmail = true;
                identityOptions.SignIn.RequireConfirmedEmail = true;
            })
            .AddErrorDescriber<MultiLanguageIdentityErrorDescriber>()
            .AddEntityFrameworkStores<DataDbContext>()
            .AddDefaultTokenProviders();

        /* ------------------COOKIE SETTINGS------------------ */
        services.ConfigureApplicationCookie(cookieOptions =>
            {
                cookieOptions.LoginPath = new PathString("/auth/login");
                cookieOptions.SlidingExpiration = true;
                cookieOptions.ExpireTimeSpan = TimeSpan.FromHours(1);
                cookieOptions.Cookie.Name = "Identity.Cookie";
                cookieOptions.AccessDeniedPath = "/Error/AccessDenied";
            });

        /* ------------------AUTOMAPPER------------------ */
        services.AddAutoMapper(typeof(AuthAutomapperProfile));

        /* ------------------OPTIONS SETTINGS------------------ */
        services.Configure<SendGridOptions>(configuration.GetSection("SendGridOptions"));
        services.Configure<WebPathsOptions>(configuration.GetSection("WebPathsOptions"));
        
        services.AddDistributedMemoryCache();
        services.AddSession(options => { options.IdleTimeout = TimeSpan.FromDays(1); });

        services.AddScoped<SeedDataDbContext>();
        
        /* ------------------REPOSITORIES------------------ */
        services.AddTransient<IUsersRepository, UsersRepository>();
        services.AddTransient<IFacultyRepository, FacultyRepository>();
        services.AddTransient<IFormatLearningRepository, FormatLearningRepository>();
        services.AddTransient<IGroupRepository, GroupRepository>();
        services.AddTransient<ILearningStatusRepository, LearningStatusRepository>();
        services.AddTransient<IQualificationRepository, QualificationRepository>();
        services.AddTransient<ISpecialityRepository, SpecialityRepository>();
        services.AddTransient<IDisciplineRepository, DisciplineRepository>();
        
        /* ------------------SERVICES------------------ */
        services.AddTransient<INotificationService, NotificationService>();
        services.AddTransient<IMailService, MailService>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUsersService, UsersService>();
        services.AddTransient<IFacultyService, FacultyService>();
        services.AddTransient<IFormatLearningService, FormatLearningService>();
        services.AddTransient<IGroupService, GroupService>();
        services.AddTransient<ILearningStatusService, LearningStatusService>();
        services.AddTransient<IQualificationService, QualificationService>();
        services.AddTransient<ISpecialityService, SpecialityService>();
        services.AddTransient<IDisciplineService, DisciplineService>();
        
        /* ------------------LOCALIZATION------------------ */
        services.AddLocalization(localizationOptions => localizationOptions.ResourcesPath = "Resources");

        services.Configure<RequestLocalizationOptions>(localizationOptions =>
        {
            const string defaultCulture = "uk";
            const string englishCultureName = "en";

            CultureInfo[] supportedCultures =
            [
                new CultureInfo(defaultCulture),
                new CultureInfo(englishCultureName)
            ];

            localizationOptions.DefaultRequestCulture = new RequestCulture(defaultCulture);
            localizationOptions.SetDefaultCulture(defaultCulture);
            localizationOptions.SupportedCultures = supportedCultures;
            localizationOptions.SupportedUICultures = supportedCultures;
        });
        
        services.AddNotyf(options =>
        {
            options.DurationInSeconds = 10;
            options.IsDismissable = true;
            options.Position = NotyfPosition.TopRight;
        });

        services.AddControllersWithViews()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder)
            .AddDataAnnotationsLocalization();
        
        return services;
    }
}