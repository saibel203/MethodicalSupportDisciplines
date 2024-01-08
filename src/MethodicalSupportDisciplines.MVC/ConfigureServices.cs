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
                const int passwordLength = 6;

                identityOptions.Password.RequiredLength = passwordLength;
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
            const string identityCookieName = "Identity.Cookie";
            const string loginPathString = "/auth/login";
            const string accessDeniedPathString = "/Error/AccessDenied";

            const int expireTime = 1;

            cookieOptions.LoginPath = new PathString(loginPathString);
            cookieOptions.SlidingExpiration = true;
            cookieOptions.ExpireTimeSpan = TimeSpan.FromHours(expireTime);
            cookieOptions.Cookie.Name = identityCookieName;
            cookieOptions.AccessDeniedPath = accessDeniedPathString;
        });

        /* ------------------AUTOMAPPER------------------ */
        services.AddAutoMapper(typeof(AuthAutomapperProfile));

        /* ------------------OPTIONS SETTINGS------------------ */
        const string sendGridOptionsSection = "SendGridOptions";
        const string webPathsOptionsSection = "WebPathsOptions";

        services.Configure<SendGridOptions>(configuration.GetSection(sendGridOptionsSection));
        services.Configure<WebPathsOptions>(configuration.GetSection(webPathsOptionsSection));

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
        services.AddTransient<IMaterialTypeRepository, MaterialTypeRepository>();
        services.AddTransient<IMaterialRepository, MaterialRepository>();
        services.AddTransient<IMaterialDisciplineMaterialRepository, MaterialDisciplineMaterialRepository>();
        services.AddTransient<IFileService, FileService>();

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
        services.AddTransient<IMaterialTypeService, MaterialTypeService>();
        services.AddTransient<IMaterialService, MaterialService>();
        services.AddTransient<IMaterialDisciplineMaterialService, MaterialDisciplineMaterialService>();

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