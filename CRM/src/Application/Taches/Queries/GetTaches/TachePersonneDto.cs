using CRM.Application.Common.Mappings;
using CRM.Domain.Entities;
using CRM.Domain.Interfaces;

namespace CRM.Application.Taches.Queries.GetTaches
{
    public class TachePersonneDto : IMapFrom<Client>
    {
        public int? Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Client, TachePersonneDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
               

            profile.CreateMap<IApplicationUser, TachePersonneDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Nom, opt => opt.MapFrom(s => s.Utilisateur.Nom))
                .ForMember(d => d.Prenom, opt => opt.MapFrom(s => s.Utilisateur.Prenom));
        }
    }
}