using AutoMapper;
using Gateway.Models.Entities;
using Gateway.Models.Post;
using Gateway.Models.Update;
using Gateway.Models.View;

namespace Gateway.Logic.Profiles;

public class SectionMapperProfile : Profile
{
    public SectionMapperProfile()
    {
        CreateMap<SectionPostDto, Section>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

        CreateMap<SectionUpdateDto, Section>();

        CreateMap<Section, SectionViewDto>()
            .ForMember(dest => dest.Topics, opt => opt.MapFrom(src => src.SectionTopics.Select(st => st.Topic.Title ?? null).ToArray()));
    }
}