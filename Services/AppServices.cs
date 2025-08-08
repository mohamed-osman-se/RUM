using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using RUM.Data;
using RUM.Interfaces;
using RUM.Repositories;
using Serilog;

namespace RUM.Services
{
    public static class AppServices
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection Services, WebApplicationBuilder builder)
        {

            Services.AddControllersWithViews();
            Services.AddScoped<IUserRepo, UserRepo>();
            Services.AddScoped<IMessageRepo, MessageRepo>();
            Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(options =>
                 {
                     options.LoginPath = "/Account/Login";
                     options.LogoutPath = "/Account/Logout";
                     options.AccessDeniedPath = "/Account/AccessDenied";
                     options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                     options.SlidingExpiration = true;
                     options.Cookie.HttpOnly = true;
                     options.Cookie.Name = "RUM.Auth";
                 });

            Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite("Data Source = app.db");
            });
            Services.AddHostedService<MessageCleanupService>();




            Services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("fixed", fixedWindowOptions =>
                {
                    fixedWindowOptions.Window = TimeSpan.FromMinutes(1);
                    fixedWindowOptions.PermitLimit = 100;
                    fixedWindowOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    fixedWindowOptions.QueueLimit = 0;
                    
                });
            });



            return Services;
        }

    }
}