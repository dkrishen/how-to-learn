using Gateway.Logic;
using Gateway.Models.PostDto;
using Gateway.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionLogic _sectionLogic;

        public SectionController(ISectionLogic sectionLogic)
        {
            _sectionLogic = sectionLogic;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _sectionLogic.GetSectionAsync(id).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _sectionLogic.GetSectionsAsync().ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost]  
        public async Task<IActionResult> Post([FromBody] SectionPostDto section)
        {
            await _sectionLogic.AddSectionAsync(section).ConfigureAwait(false);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            await _sectionLogic.DeleteSectionAsync(id).ConfigureAwait(false);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SectionUpdateDto section)
        {
            await _sectionLogic.UpdateSectionAsync(section).ConfigureAwait(false);
            return Ok();
        }
    }
}
