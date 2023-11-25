﻿using System.Globalization;
using AspNetCoreHero.ToastNotification;
using MethodicalSupportDisciplines.BLL.Infrastructure.ErrorDescribers;
using MethodicalSupportDisciplines.BLL.Infrastructure.MappingProfiles;
using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.Core.Models.Identity;
using MethodicalSupportDisciplines.BLL.Services;
using MethodicalSupportDisciplines.Core.IOptions;
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

        services.ConfigureApplicationCookie(cookieOptions =>
            {
                cookieOptions.LoginPath = new PathString("/auth/login");
                cookieOptions.SlidingExpiration = true;
                cookieOptions.ExpireTimeSpan = TimeSpan.FromHours(1);
                cookieOptions.Cookie.Name = "Identity.Cookie";
                cookieOptions.AccessDeniedPath = "/Error/AccessDenied";
            });

        services.AddAutoMapper(typeof(AuthAutomapperProfile));

        services.Configure<SendGridOptions>(configuration.GetSection("SendGridOptions"));
        services.Configure<WebPathsOptions>(configuration.GetSection("WebPathsOptions"));
        
        services.AddDistributedMemoryCache();
        services.AddSession(options => { options.IdleTimeout = TimeSpan.FromDays(1); });

        services.AddScoped<SeedDataDbContext>();
        
        services.AddTransient<INotificationService, NotificationService>();
        services.AddTransient<IMailService, MailService>();
        services.AddTransient<IAuthService, AuthService>();

        services.AddLocalization(localizationOptions => localizationOptions.ResourcesPath = "Resources");

        services.Configure<RequestLocalizationOptions>(localizationOptions =>
        {
            const string defaultCulture = "uk";
            const string englishCultureName = "en";

            CultureInfo[] supportedCultures =
            {
                new(defaultCulture),
                new(englishCultureName)
            };

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