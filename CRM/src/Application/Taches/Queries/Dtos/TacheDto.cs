
using System;
using CRM.Application.Clients.Queries.GetClients;
using CRM.Application.Common.Mappings;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using CRM.Domain.Interfaces;

namespace CRM.Application.Taches.Queries.Dtos
{
    public class TacheDto: IMapFrom<Tache>
    {
        public int IdTache { get; set; }
        
        public string Nom { get; set; }
        
        public string Description { get; set; }
        
        public int? IdClient { get; set; }
        
        public int IdUtilisateur { get; set; }
        
        public DateTime? Echeance { get; set; }
        
        public StatutTache StatutTache { get; set; }
        
        public bool EnRetard { get; set; }
        
        public decimal? GainPotentiel { get; set; }
        public ClientDto Client { get; set; }
        public IApplicationUser Responsable { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {

            profile.CreateMap<Tache, TacheDto>()
                .ForMember(d => d.Client, opt => opt.MapFrom(x => x.Client))
                // .ForMember(d => d.Responsable, opt => opt.MapFrom(x => x.UtilisateurNavigation ?? x.Client.Responsable))
                .ForMember(d => d.StatutTache, opt => opt.MapFrom(x => (StatutTache)x.StatutTache))
                // id tache
                .ForMember(d => d.IdTache, opt => opt.MapFrom(s => s.Id))
                //si tache en retard
                .ForMember(d => d.EnRetard, opt => opt.MapFrom(t => t.Echeance < DateTime.Now));

        }
    }
}