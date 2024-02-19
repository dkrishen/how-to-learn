using Gateway.Models.Elastic;
using Nest;

namespace Gateway.Core.Extenshions;

public static class ElasticSearchExtenshions
{
    public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
    {
        var url = configuration["Data:ElasticConfiguration:Uri"];
        var defaultIndex = configuration["Data:ElasticConfiguration:Index"];

        var settings = new ConnectionSettings(new Uri(url))
            .PrettyJson()
            .DefaultIndex(defaultIndex);

        AddDefaultMappings(settings);

        var client = new ElasticClient(settings);
        services.AddSingleton<IElasticClient>(client);

        CreateIndex(client, defaultIndex);
    }

    private static void AddDefaultMappings(ConnectionSettings settings)
    {
        settings.DefaultMappingFor<TopicElasticDto>(t => 
            t/*.Ignore(x => x.Id)*/);
    }

    private static void CreateIndex(IElasticClient client, string indexName)
    {
        client.Indices.Create(indexName, i => i.Map<TopicElasticDto>(x => x.AutoMap()));
    }
}
