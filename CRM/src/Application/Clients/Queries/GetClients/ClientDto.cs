using CRM.Application.Common.Mappings;
using CRM.Domain.Entities;
using CRM.Domain.Enums;

namespace CRM.Application.Clients.Queries.GetClients
{
    public class ClientDto : IMapFrom<Client>
    {
        public int IdClient { get; set; }
        
        public string Nom { get; set; }
        
        public string Prenom { get; set; }
        
        public int IdResponsable { get; set; }
        
        public TypeClient Type { get; set; }
        
        public CategorieClient Categorie { get; set; }
        
        public Civilite Civilite { get; set; }
        

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Client, ClientDto>()
                .ForMember(d => d.IdResponsable, opt => opt.MapFrom(s => s.IdResponsable))
                .ForMember(d=> d.Civilite, opt => opt.MapFrom(s=> (Civilite) s.Civilite))
                .ForMember(d=> d.Type, opt => opt.MapFrom(s=> (TypeClient) s.Type))
                .ForMember(d=> d.Categorie, opt => opt.MapFrom(s=> (CategorieClient) s.Categorie));
            
        }
    }
}