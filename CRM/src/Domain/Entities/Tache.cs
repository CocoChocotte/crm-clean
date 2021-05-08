using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.Domain.Common;
using CRM.Domain.Enums;
using CRM.Domain.Interfaces;

namespace CRM.Domain.Entities
{
    public class Tache : AuditableEntity
    {
        public int Id { get; set; }
        
        public string Nom { get; set; }
        
        public string Description { get; set; }
        
        public int? IdClient { get; set; }
        
        public int IdUtilisateur { get; set; }
        
        public DateTime? Echeance { get; set; }
        
        public StatutTache StatutTache { get; set; }
        
        public decimal? GainPotentiel { get; set; }
        
        [ForeignKey("IdClient")]
        public virtual Client Client { get; set; }
        
        [ForeignKey("IdUtilisateur")]
        public virtual Utilisateur Utilisateur { get; set; }
    }
}