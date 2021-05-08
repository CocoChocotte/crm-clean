using CRM.Domain.Common;

namespace CRM.Domain.Entities
{
    public class Lieu : AuditableEntity
    {
        public int Id { get; set; }
        
        public string Adresse { get; set; }
        
        public string Ville { get; set; }
        
        public string CodePostal { get; set; }
        
        public string ComplementAdresse { get; set; }
    }
}