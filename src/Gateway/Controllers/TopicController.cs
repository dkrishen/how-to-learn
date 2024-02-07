using Gateway.Logic;
using Gateway.Models.Post;
using Gateway.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers;

[Route("api/[controller]")]
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
    public async Task<IActionResult> Get()
    {
        var result = await _topicLogic.GetAsync().ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet("BySection/{id}")]
    public async Task<IActionResult> GetBySection(Guid id)
    {
        var result = await _topicLogic.GetAsync().ConfigureAwait(false);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TopicPostDto topic)
    {
        await _topicLogic.AddAsync(topic).ConfigureAwait(false);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] Guid id)
    {
        await _topicLogic.RemoveAsync(id).ConfigureAwait(false);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] TopicUpdateDto topic)
    {
        await _topicLogic.UpdateAsync(topic).ConfigureAwait(false);
        return Ok();
    }
}
