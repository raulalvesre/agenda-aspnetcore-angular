using System;
using System.Collections.Generic;
using System.Security.Claims;
using Agenda.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Agenda.API.Extensions
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public int GetUserId()
        {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetUserId() : 0;
        }

        public string GetUsername()
        {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetUsername() : "";
        }

        public string GetUserEmail()
        {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : "";
        }

        public string GetUserRole()
        {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetUserRole() : "";
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool IsInRole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }

    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(claim?.Value);
        }

        public static string GetUsername(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.Name);
            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }

        public static string GetUserRole(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.Role);
            return claim?.Value;
        }
    }
}
