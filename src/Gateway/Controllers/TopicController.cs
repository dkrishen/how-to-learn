using Gateway.Core.Models;
using Gateway.Logic;
using Gateway.Models.Delete;
using Gateway.Models.Post;
using Gateway.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers;

//[Route("api/[controller]")]
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
        var result = await _topicLogic.GetAsync(new Queries()
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
}
