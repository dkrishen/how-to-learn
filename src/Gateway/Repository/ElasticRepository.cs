using Gateway.Models.Elastic;
using Nest;

namespace Gateway.Repository;

public class ElasticRepository : IElasticRepository
{
    public ElasticRepository(IElasticClient client, IConfiguration configuration)
    {
        _client = client;
        _indexName = configuration["Data:ElasticConfiguration:Index"];
        _analyzerName = configuration["Data:ElasticConfiguration:Analyzer"];
    }

    private readonly IElasticClient _client;
    private readonly string _indexName;
    private readonly string _analyzerName;

    public async Task<ISearchResponse<TopicElasticDto>> SearchAsync(string key)
        => await _client.SearchAsync<TopicElasticDto>(s =>
                s.Query(q =>
                    q.MultiMatch(m => m
                        .Fields(f => f
                            .Field("description")
                            .Field("title")
                        ).Query(key)
                    )
                )
                .Sort(sort => sort.Descending("_score"))
                .Size(1000)
                .TrackTotalHits(true)
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
        => await _client.DeleteAsync<TopicElasticDto>(id)
            .ConfigureAwait(false);

    public async Task<AnalyzeResponse> AnalyzeDocumentAsync(string text)
        => await _client.Indices.AnalyzeAsync(a => a
                .Index(_indexName)
                .Analyzer(_analyzerName)
                .Text(text)
            ).ConfigureAwait(false);
}