using CRM.Domain.Common;

namespace CRM.Domain.Entities
{
    public class Email : AuditableEntity
    {
        public int Id { get; set; }
        
        public string Mail { get; set; }
    }
}