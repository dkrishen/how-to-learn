using Gateway.Models.Elastic;
using Nest;

namespace Gateway.Core.Extenshions;

public static class ElasticSearchExtenshions
{
    public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
    {
        var url = configuration["Data:ElasticConfiguration:Uri"];
        var defaultIndex = configuration["Data:ElasticConfiguration:Index"];
        var analyzerName = configuration["Data:ElasticConfiguration:Analyzer"];

        var settings = new ConnectionSettings(new Uri(url))
            .PrettyJson()
            .DefaultIndex(defaultIndex);

        AddDefaultMappings(settings);

        var client = new ElasticClient(settings);
        services.AddSingleton<IElasticClient>(client);

        CreateIndex(client, defaultIndex, analyzerName);
    }

    private static void AddDefaultMappings(ConnectionSettings settings)
    {
        var setDefaultMappingResponse = settings
            .DefaultMappingFor<TopicElasticDto>(t => t);
    }

    private static void CreateIndex(IElasticClient client, string indexName, string analyzerName)
    {
        var stopWords = new StopWords(new List<string>()
        {
            "а", "без", "более", "бы", "был", "была", "были", "было", "быть", "в", "вам",
            "вас", "весь", "во", "вот", "все", "всего", "всех", "вы", "где", "да", "даже",
            "для", "до", "его", "ее", "если", "есть", "еще", "же", "за", "здесь", "и", "из",
            "или", "им", "их", "к", "как", "ко", "когда", "кто", "ли", "либо", "мне",
            "может", "мы", "на", "надо", "наш", "не", "него", "нее", "нет", "ни", "них",
            "но", "ну", "о", "об", "однако", "он", "она", "они", "оно", "от", "очень", "по",
            "под", "при", "с", "со", "так", "также", "такой", "там", "те", "тем", "то", "того",
            "тоже", "той", "только", "том", "ты", "у", "уже", "хотя", "чего", "чей", "чем",
            "что", "чтобы", "чье", "чья", "эта", "эти", "это", "я", "a", "an", "and", "are",
            "as", "at", "be", "but", "by", "for", "if", "in", "into", "is", "it", "no", "not",
            "of", "on", "or", "such", "that", "the", "their", "then", "there", "these", "they",
            "this", "to", "was", "will", "with"
        });

        var createIndexResponse = client.Indices.Create(indexName, i => i
            .Settings(s => s
                .Analysis(a => a
                    .Analyzers(an => an
                        .Custom(analyzerName, ca => ca
                            .Tokenizer("russian_tokenizer")
                            .Filters("lowercase", 
                                     "russian_stop", 
                                     "russian_stemmer")
                        )
                    )
                    .Tokenizers(t => t
                        .Standard("russian_tokenizer")
                    )
                    .TokenFilters(tf => tf
                        .Stop("russian_stop", sf => sf
                            .StopWords(stopWords))
                        .Stemmer("russian_stemmer", sf => sf
                            .Language(Language.Russian.ToString())
                        )
                    )
                )
            )
            .Map<TopicElasticDto>(x => x
                .Properties(p => p
                    .Text(t => t
                        .Name(nameof(TopicElasticDto.Title))
                        .Analyzer("russian_analyzer")
                        .SearchAnalyzer("russian_analyzer")
                        .SearchQuoteAnalyzer("russian_analyzer")
                    ).Text(t => t
                        .Name(nameof(TopicElasticDto.Description))
                        .Analyzer("russian_analyzer")
                        .SearchAnalyzer("russian_analyzer")
                        .SearchQuoteAnalyzer("russian_analyzer")
                    ).Keyword(t => t
                        .Name(nameof(TopicElasticDto.Id))
                    )
                )
            )
        );
    }
}