using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }

        DbSet<TodoItem> TodoItems { get; set; }
        
        DbSet<Client> Clients { get; set; }
        
        DbSet<Declarant> Declarants { get; set; }

        DbSet<Societe> Societes { get; set; }
        
        DbSet<Tache> Taches { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}