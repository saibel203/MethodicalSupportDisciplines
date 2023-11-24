﻿using MethodicalSupportDisciplines.BLL.Infrastructure.MappingProfiles;
using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.BLL.Models.Identity;
using MethodicalSupportDisciplines.BLL.Services;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Identity;

namespace MethodicalSupportDisciplines.MVC;

public static class ConfigureServices
{
    public static IServiceCollection AddBasicsWebServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddIdentity<ApplicationUser, ApplicationRole>(identityOptions =>
            {
                identityOptions.Password.RequiredLength = 6;
                identityOptions.Password.RequireLowercase = true;
                identityOptions.Password.RequireUppercase = true;
                identityOptions.Password.RequireDigit = true;
                identityOptions.Password.RequireNonAlphanumeric = false;

                // identityOptions.User.RequireUniqueEmail = true;
                // identityOptions.SignIn.RequireConfirmedEmail = true;
            })
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

        services.AddAutoMapper(typeof(AutomapperProfile));
        
        services.AddDistributedMemoryCache();
        services.AddSession(options => { options.IdleTimeout = TimeSpan.FromDays(1); });

        services.AddTransient<IAuthService, AuthService>();

        services.AddControllersWithViews();
        
        return services;
    }
}