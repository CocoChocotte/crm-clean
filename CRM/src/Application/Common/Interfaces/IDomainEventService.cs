using CRM.Domain.Common;
using System.Threading.Tasks;

namespace CRM.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}