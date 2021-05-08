using CRM.Domain.Entities;

namespace CRM.Domain.Interfaces
{
    public interface IApplicationUser
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
        
        public int IdUtilisateur { get; set; }
        
        public Utilisateur Utilisateur { get; set; }
    }
}