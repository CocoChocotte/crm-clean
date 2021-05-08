using CRM.Domain.Common;
using CRM.Domain.Enums;

namespace CRM.Domain.Entities
{
    public class Telephone : AuditableEntity
    {
        public int Id { get; set; }
        
        public string Numero { get; set; }
        
        public TypeTelephone Type { get; set; }
    }
}