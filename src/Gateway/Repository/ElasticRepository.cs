using Gateway.Models.Elastic;
using Nest;

namespace Gateway.Repository;

public class ElasticRepository : IElasticRepository
{
    public ElasticRepository(IElasticClient client, IConfiguration configuration)
    {
        _client = client;
    }

    private readonly IElasticClient _client;

    public async Task<ISearchResponse<TopicElasticDto>> SearchAsync(string key)
        => await _client.SearchAsync<TopicElasticDto>(s =>
            s.Query(q =>
                q.QueryString(d =>
                    d.Query('*' + key + '*')
                )   
            ).Size(1000)
        ).ConfigureAwait(false);

    public async Task<ISearchResponse<TopicElasticDto>> SearchAllAsync()
        => await _client.SearchAsync<TopicElasticDto>(s =>
            s.Query(q =>
                q.MatchAll()
            ).Size(1000)
        ).ConfigureAwait(false);

    public async Task<IndexResponse> IndexDocumentAsync(TopicElasticDto topic)
        => await _client.IndexDocumentAsync(topic)
            .ConfigureAwait(false);

    public async Task<UpdateResponse<TopicElasticDto>> UpdateDocumentAsync(TopicElasticDto topic)
        => await _client.UpdateAsync<TopicElasticDto, object>(topic.Id, u => 
            u.Doc(topic)
        ).ConfigureAwait(false);

    public async Task<DeleteResponse> DeleteDocumentAsync(Guid id)
        => await _client.DeleteAsync<TopicElasticDto>(id).ConfigureAwait(false);
}
