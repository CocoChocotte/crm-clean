namespace CRM.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string IdentityId { get; }

        int SocieteId { get; set; }
        
        int UserId { get; set; }

        bool IsSuperAdmin { get; set; }
    }
}