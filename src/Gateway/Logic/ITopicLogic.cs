using Gateway.Models.Post;
using Gateway.Models.Update;
using Gateway.Models.View;

namespace Gateway.Logic;

public interface ITopicLogic : ILogicCrud<TopicViewDto, TopicPostDto, TopicUpdateDto>
{
    public Task<TopicViewDto[]> GetTopicsBySectionAsync(Guid sectionId);

    public Task<IEnumerable<TopicViewDto>> SearchAsync(string key);
    public Task<IEnumerable<TopicViewDto>> SearchAsync();
    public Task<string> AnalyzeAsync(string str);
    public Task IndexDocumentAsync(TopicPostDto topic);
    public Task UpdateDocumentAsync(TopicUpdateDto topic);
    public Task DeleteDocumentAsync(Guid id);
}
