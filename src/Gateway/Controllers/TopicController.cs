using Gateway.Core.Models;
using Gateway.Logic;
using Gateway.Models.Delete;
using Gateway.Models.Elastic;
using Gateway.Models.Post;
using Gateway.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers;

[Route("api/topic")]
[ApiController]
public class TopicController : ControllerBase
{
    private readonly ITopicLogic _topicLogic;

    public TopicController(ITopicLogic topicLogic)
    {
        _topicLogic = topicLogic;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _topicLogic.GetAsync(id).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get(int page = 0, int pageSize = 20, string? pattern = null)
    {
        var result = await _topicLogic.GetAsync(new QueriesRequestDto()
        {
            Page = page,
            PageSize = pageSize,
            Pattern = pattern
        }).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet("by-section/{id}")]
    public async Task<IActionResult> GetBySection(Guid id)
    {
        var result = await _topicLogic.GetTopicsBySectionAsync(id).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TopicPostDto topic)
    {
        await _topicLogic.AddAsync(topic).ConfigureAwait(false);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] TopicDeleteDto topic)
    {
        await _topicLogic.RemoveAsync(topic.Id).ConfigureAwait(false);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] TopicUpdateDto topic)
    {
        await _topicLogic.UpdateAsync(topic).ConfigureAwait(false);
        return Ok();
    }

    #region elastic

    [HttpGet("elastic/{key}")]
    public async Task<IActionResult> Search(string key)
    {
        var result = await _topicLogic.SearchAsync(key).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet("elastic")]
    public async Task<IActionResult> SearchAll()
    {
        var result = await _topicLogic.SearchAsync().ConfigureAwait(false);
        return Ok(result);
    }

    [HttpPost("elasic")]
    public async Task<IActionResult> PostTopic([FromBody] TopicPostDto topic)
    {
        await _topicLogic.IndexDocumentAsync(topic).ConfigureAwait(false);
        return Ok();
    }

    [HttpPost("elasic/analyze")]
    public async Task<IActionResult> AnalyzeText([FromBody] TextAnalysisDto value)
    {
        var result = await _topicLogic.AnalyzeAsync(value.Text).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpPut("elasic")]
    public async Task<IActionResult> PutTopic([FromBody] TopicUpdateDto topic)
    {
        await _topicLogic.UpdateDocumentAsync(topic).ConfigureAwait(false);
        return Ok();
    }

    [HttpDelete("elasic")]
    public async Task<IActionResult> DeleteTopic([FromBody] Guid id)
    {
        await _topicLogic.DeleteDocumentAsync(id).ConfigureAwait(false);
        return Ok();
    }

    #endregion
}