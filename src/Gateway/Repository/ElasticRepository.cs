using Gateway.Models.Entities;

namespace Gateway.Repository;

public class ElasticRepository : IElasticRepository
{
    public ElasticRepository() { }

    public Task CheckIndexAsync(string indexName)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdDocumentAsync(string indexName, Topic topic)
    {
        throw new NotImplementedException();
    }

    public Task DeleteIndexAsync(string indexName)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> GetDocumentAsync(string indexName, string id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<List<Topic>> GetDocumentsAsync(string indexName)
    {
        throw new NotImplementedException();
    }

    public Task InsertBuldDocumentsAsync(string indexName, IEnumerable<Topic> topics)
    {
        throw new NotImplementedException();
    }

    public Task InsertDocumentAsync(string indexName, Topic topic)
    {
        throw new NotImplementedException();
    }
}
