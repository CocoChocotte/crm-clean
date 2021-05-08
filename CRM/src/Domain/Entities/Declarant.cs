using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.Domain.Common;
using CRM.Domain.Enums;

#nullable disable

namespace CRM.Domain.Entities
{
    public class Declarant : AuditableEntity
    {
        public int Id { get; set; }
        
        public Civilite Civilite { get; set; }

        public string Nom { get; set; }
        
        public string Prenom { get; set; }
        
        public DateTime? DateNaissance { get; set; }
        
        public string Profession { get; set; }
        
        public SituationFamiliale SituationFamiliale { get; set; }
        
        public Telephone TelephoneMobile { get; set; }
        
        public Telephone TelephoneProfessionnel { get; set; }
        
        public Telephone TelephoneDomicile { get; set; }
        
        public Email EmailPersonnel { get; set; }
        
        public Email EmailProfessionnel { get; set; }
        
        public ICollection<Lieu> Adresses { get; set; }
        
        public int IdClient { get; set; }
        
        public TypeDeclarant TypeDeclarant { get; set; }
        
        [ForeignKey("IdClient")]
        public virtual Client Client { get; set; }
    }
}