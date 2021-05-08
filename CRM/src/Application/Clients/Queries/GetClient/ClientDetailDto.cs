using System;
using CRM.Application.Clients.Queries.GetClients;
using CRM.Application.Common.Mappings;
using CRM.Domain.Entities;
using CRM.Domain.Enums;

namespace CRM.Application.Clients.Queries.GetClient
{
    public class ClientDetailDto : IMapFrom<Client>
    {
        public int IdClient { get; set; }
        
        public string Nom { get; set; }
        
        public string Prenom { get; set; }
        
        public DateTime DateDeNaissance { get; set; }
        
        public string Commentaire { get; set; }
        
        public int IdResponsable { get; set; }
        
        public TypeClient Type { get; set; }
        
        public CategorieClient Categorie { get; set; }
        
        public string Email { get; set; }
        
        public Civilite Civilite { get; set; }
        
        public string Adresse { get; set; }
        
        public string CpltAdresse { get; set; }
        
        public string CodePostal { get; set; }
        
        public string Ville { get; set; }
        
        public string Telephone { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Client, ClientDetailDto>()
                .ForMember(d => d.IdResponsable, opt => opt.MapFrom(s => s.IdResponsable))
                .ForMember(d=> d.Civilite, opt => opt.MapFrom(s=> (Civilite) s.Civilite))
                .ForMember(d=> d.Categorie, opt => opt.MapFrom(s=> (CategorieClient) s.Categorie));

        }
    }
}