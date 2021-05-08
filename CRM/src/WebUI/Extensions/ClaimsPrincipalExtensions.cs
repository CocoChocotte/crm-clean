using System;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using CRM.Application.Common.Models;

namespace CRM.WebUI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsSuperAdmin(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.HasClaim(ClaimTypes.Role, Constants.UserRoles.SuperAdmin);
        }

        public static int GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var id = principal.FindFirstValue(Constants.UserCustomClaims.UserId);

            if (string.IsNullOrEmpty(id))
            {
                throw new AuthenticationException();
            }

            return int.Parse(id);
        }

        public static int GetSocieteId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var id = principal.FindFirstValue(Constants.UserCustomClaims.SocieteId);

            if (string.IsNullOrEmpty(id))
            {
                throw new AuthenticationException();
            }

            return int.Parse(id);
        }

        public static bool IsAuthorizeApplication(this ClaimsPrincipal principal, string applicationCode)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            if (principal.IsSuperAdmin())
            {
                return true;
            }

            var apps = principal.FindAll(x => x.Type == Constants.UserCustomClaims.ApplicationAuthorisations);

            if (apps == null)
            {
                throw new AuthenticationException();
            }

            return apps.Any(x => x.Value == applicationCode);
        }
    }
}