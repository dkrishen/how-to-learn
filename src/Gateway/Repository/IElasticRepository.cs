using Gateway.Models.Elastic;
using Gateway.Models.Entities;
using Nest;

namespace Gateway.Repository
{
    public interface IElasticRepository
    {
        Task<ISearchResponse<TopicElasticDto>> SearchAsync(string key);
        Task<ISearchResponse<TopicElasticDto>> SearchAllAsync();
        Task<IndexResponse> IndexDocumentAsync(TopicElasticDto topic);
        Task<UpdateResponse<TopicElasticDto>> UpdateDocumentAsync(TopicElasticDto topic);
        Task<DeleteResponse> DeleteDocumentAsync(Guid id);
    }
}
