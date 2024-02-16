using Gateway.Models.Post;
using Gateway.Models.Update;
using Gateway.Models.View;

namespace Gateway.Logic;

public interface ITopicLogic : ILogicCrud<TopicViewDto, TopicPostDto, TopicUpdateDto>
{
    public Task<TopicViewDto[]> GetTopicsBySectionAsync(Guid sectionId);
}
