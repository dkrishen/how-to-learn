using AutoMapper;
using Gateway.Models.Entities;
using Gateway.Models.PostDto;
using Gateway.Models.Update;
using Gateway.Models.View;

namespace Gateway.Logic.Profiles
{
    public class SectionMapperProfile : Profile
    {
        public SectionMapperProfile()
        {
            CreateMap<SectionPostDto, Section>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));

            CreateMap<SectionUpdateDto, Section>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));

            CreateMap<Section, SectionViewDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Topics, opt => opt.MapFrom(src => src.SectionTopics.Select(st => st.Section.Title ?? null)));
        }
    }
}
