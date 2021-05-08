using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.Domain.Common;
using CRM.Domain.Interfaces;

#nullable disable

namespace CRM.Domain.Entities
{
    public partial class Societe : AuditableEntity
    {
        public Societe()
        {
            Utilisateurs = new HashSet<Utilisateur>();
        }

        public int Id { get; set; }
        
        public string Nom { get; set; }
        
        public string Site { get; set; }
        
        public bool Actif { get; set; }
        
        public string Commentaire { get; set; }
        
        public DateTime? DebutContrat { get; set; }
        
        public DateTime? FinContrat { get; set; }
        
        public int? DureeContrat { get; set; }
        
        [InverseProperty("Societe")]
        public virtual ICollection<Utilisateur> Utilisateurs { get; set; }
    }
}