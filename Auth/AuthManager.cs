using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using RUM.Models;

namespace RUM.Auth
{
    public static class AuthManager
    {
        public static async Task Auth(HttpContext httpContext, User user)
        {
            var claims = new List<Claim>
        {
            new Claim("Fname", user.Fname!),
            new Claim("Lname",user.Lname!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim("UserId", user.Id.ToString()),
        };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}