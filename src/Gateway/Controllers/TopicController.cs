using Gateway.Logic;
using Gateway.Models.PostDto;
using Gateway.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
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
            var result = await _topicLogic.GetTopicAsync(id).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _topicLogic.GetTopicsAsync().ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TopicPostDto topic)
        {
            await _topicLogic.AddTopicAsync(topic).ConfigureAwait(false);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            await _topicLogic.DeleteTopicAsync(id).ConfigureAwait(false);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TopicUpdateDto topic)
        {
            await _topicLogic.UpdateTopicAsync(topic).ConfigureAwait(false);
            return Ok();
        }
    }
}
