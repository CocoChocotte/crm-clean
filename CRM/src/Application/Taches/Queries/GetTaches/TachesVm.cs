using System.Collections.Generic;
using CRM.Application.Taches.Queries.Dtos;

namespace CRM.Application.Taches.Queries.GetTaches
{
    public class TachesVm
    {
        public List<TacheDto> TachesAFaire { get; set; }
        
        public List<TacheDto> TachesEncours { get; set; }

        public List<TacheDto> TachesTerminees { get; set; }

        public List<TacheDto> TachesEnRetard { get; set; }

    }
}