using Gateway.Models.Entities;

namespace Gateway.Repository
{
    public interface IElasticRepository
    {
        Task CheckIndexAsync(string indexName);
        Task InsertDocumentAsync(string indexName, Topic topic);
        Task DeleteIndexAsync(string indexName);
        Task DeleteByIdDocumentAsync(string indexName, Topic topic);
        Task InsertBuldDocumentsAsync(string indexName, IEnumerable<Topic> topics);
        Task<Topic> GetDocumentAsync(string indexName, string id);
        IEnumerable<List<Topic>> GetDocumentsAsync(string indexName);
    }
}
