using AutoMapper;
using Gateway.Models.Entities;
using Gateway.Models.Post;
using Gateway.Models.Update;
using Gateway.Models.View;

namespace Gateway.Logic.Profiles;

public class KeyMapperProfile : Profile
{
    public KeyMapperProfile()
    {
        CreateMap<KeyPostDto, Key>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<KeyUpdateDto, Key>();
        CreateMap<Key, KeyViewDto>()
            .ForMember(dest => dest.Topic, opt => opt.MapFrom(src => src.Reference.ToString()));

    }
}
