using Gateway.Models.PostDto;
using Gateway.Models.Update;
using Gateway.Models.View;

namespace Gateway.Logic
{
    public interface ITopicLogic
    {
        public Task<TopicViewDto> GetTopicAsync(Guid id);
        public Task<IEnumerable<TopicViewDto>> GetTopicsAsync();
        public Task AddTopicAsync(TopicPostDto topic);
        public Task UpdateTopicAsync(TopicUpdateDto topic);
        public Task DeleteTopicAsync(Guid id);
    }
}
