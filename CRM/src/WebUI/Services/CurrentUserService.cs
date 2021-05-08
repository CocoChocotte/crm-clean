using System;
using CRM.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using CRM.WebUI.Extensions;

namespace CRM.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string IdentityId { get; }

        public int SocieteId { get; set; }
        
        public int UserId { get; set; }

        public bool IsSuperAdmin { get; set; }
        
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            IdentityId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                SocieteId = httpContextAccessor.HttpContext?.User.GetSocieteId() ?? 0;
                UserId = httpContextAccessor.HttpContext?.User.GetUserId() ?? 0;
                IsSuperAdmin = httpContextAccessor.HttpContext?.User.IsSuperAdmin() ?? false;
            }
            catch (Exception)
            {
                SocieteId = 0;
            }
        }

    }
}