using System;
using System.Collections.Generic;
using CRM.Domain.Entities;
using CRM.Domain.Enums;

namespace CRM.Application.Clients.Queries.GetClientDeclarants
{
    public class DeclarantDto
    {
        public int Id { get; set; }
        
        public Civilite Civilite { get; set; }

        public string Nom { get; set; }
        
        public string Prenom { get; set; }
        
        public DateTime? DateNaissance { get; set; }
        
        public string Profession { get; set; }
        
        public SituationFamiliale SituationFamiliale { get; set; }
        
        public Telephone TelephoneMobile { get; set; }
        
        public Telephone TelephoneProfessionnel { get; set; }
        
        public Telephone TelephoneDomicile { get; set; }
        
        public Email EmailPersonnel { get; set; }
        
        public Email EmailProfessionnel { get; set; }
        
        public List<Lieu> Adresses { get; set; }
        
        public int IdClient { get; set; }
        
        public TypeDeclarant TypeDeclarant { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Declarant, DeclarantDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Civilite, opt => opt.MapFrom(s => (Civilite) s.Civilite))
                .ForMember(d => d.SituationFamiliale,
                    opt => opt.MapFrom(s => (SituationFamiliale) s.SituationFamiliale))
                .ForMember(d => d.Civilite, opt => opt.MapFrom(s => (Civilite) s.Civilite))
                .ForMember(d => d.TypeDeclarant, opt => opt.MapFrom(s => (Civilite) s.TypeDeclarant));


        }
    }
}