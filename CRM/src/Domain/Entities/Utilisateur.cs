using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.Domain.Common;
using CRM.Domain.Enums;

namespace CRM.Domain.Entities
{
    public class Utilisateur : AuditableEntity

    {
    public int Id { get; set; }

    public string Nom { get; set; }

    public string Prenom { get; set; }

    public int? IdSociete { get; set; }

    public Civilite Civilite { get; set; }

    public List<Lieu> Adresses { get; set; }

    public List<Telephone> Telephones { get; set; }

    [ForeignKey("IdSociete")] public virtual Societe Societe { get; set; }

    public virtual ICollection<Client> Clients { get; set; }
    }
}