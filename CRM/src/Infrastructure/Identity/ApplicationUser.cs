using System.ComponentModel.DataAnnotations.Schema;
using CRM.Domain.Entities;
using CRM.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CRM.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        public int IdUtilisateur { get; set; }
        
        [ForeignKey("IdUtilisateur")]
        public Utilisateur Utilisateur { get; set; }
    }
}