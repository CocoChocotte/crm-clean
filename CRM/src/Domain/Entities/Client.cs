using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.Domain.Common;
using CRM.Domain.Enums;
using CRM.Domain.Interfaces;

#nullable disable

namespace CRM.Domain.Entities
{
    public class Client : AuditableEntity
    {
        public Client()
        {
            Taches = new HashSet<Tache>();
        }

        public int Id { get; set; }
        
        public string Nom { get; set; }
        
        public string Prenom { get; set; }
        
        public DateTime DateDeNaissance { get; set; }
        
        public string Commentaire { get; set; }
        
        public int  IdResponsable { get; set; }
        
        public TypeClient Type { get; set; }
        
        public CategorieClient Categorie { get; set; }
        
        public ICollection<Email> Emails { get; set; }
        
        public Civilite Civilite { get; set; }
        
        public ICollection<Lieu> Adresses { get; set; }

        public ICollection<Telephone> Telephones { get; set; }
        
        public virtual ICollection<Tache> Taches { get; set; }
        
        [ForeignKey("IdResponsable")]
        public virtual Utilisateur Responsable { get; set; }
    }
}