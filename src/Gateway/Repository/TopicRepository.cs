using Gateway.Data;
using Gateway.Models.Entities;

namespace Gateway.Repository
{
    public class TopicRepository : RepositoryBase, ITopicRepository
    {
        public TopicRepository(HowToLearnDbContext context) : base(context) { }

        public async Task<Topic> GetTopicAsync(Guid id)
            => await this.GetOperationAsync<Topic>(id).ConfigureAwait(false);

        public async Task<IEnumerable<Topic>> GetTopicsAsync()
            => await this.GetOperationAsync<Topic>().ConfigureAwait(false);

        public async Task AddTopicAsync(Topic topic)
            => await this.AddOperationAsync(topic).ConfigureAwait(false);

        public async Task RemoveTopicAsync(Guid id)
            => await this.RemoveOperationAsync<Topic>(id).ConfigureAwait(false);

        public async Task UpdateTopicAsync(Topic updatedTopic)
            => await this.UpdateOperationAsync(updatedTopic).ConfigureAwait(false);
    }
}