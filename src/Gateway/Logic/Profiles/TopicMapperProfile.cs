using AutoMapper;
using Gateway.Models.Entities;
using Gateway.Models.Post;
using Gateway.Models.Update;
using Gateway.Models.View;

namespace Gateway.Logic.Profiles;

public class TopicMapperProfile : Profile
{
    public TopicMapperProfile()
    {
        CreateMap<TopicPostDto, Topic>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

        CreateMap<TopicUpdateDto, Topic>();

        CreateMap<Topic, TopicViewDto>();
    }
}
