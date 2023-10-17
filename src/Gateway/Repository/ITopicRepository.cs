using Gateway.Models.Entities;

namespace Gateway.Repository
{
    public interface ITopicRepository
    {
        public Task<Topic> GetTopicAsync(Guid id);
        public Task<IEnumerable<Topic>> GetTopicsAsync();
        public Task AddTopicAsync(Topic section);
        public Task RemoveTopicAsync(Guid id);
        public Task UpdateTopicAsync(Topic section);
    }
}
