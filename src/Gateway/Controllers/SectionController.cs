using Gateway.Core.Models;
using Gateway.Logic;
using Gateway.Models.Delete;
using Gateway.Models.Post;
using Gateway.Models.Update;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace Gateway.Controllers;

//[Route("api/[controller]")]
[Route("api/section")]
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
        var result = await _sectionLogic.GetAsync(id).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get(int page = 0, int pageSize = 20)
    {
        var result = await _sectionLogic.GetAsync(new Queries()
        {
            Page = page,
            PageSize = pageSize,
            Pattern = null
        }).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpPost("/by-data")]
    public async Task<IActionResult> Get([FromBody] PostData data)
    {
        var result = await _sectionLogic.GenerateResponse(data.Description).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpPost]  
    public async Task<IActionResult> Post([FromBody] SectionPostDto section)
    {
        await _sectionLogic.AddAsync(section).ConfigureAwait(false);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] SectionDeleteDto section)
    {
        await _sectionLogic.RemoveAsync(section.Id).ConfigureAwait(false);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] SectionUpdateDto section)
    {
        await _sectionLogic.UpdateAsync(section).ConfigureAwait(false);
        return Ok();
    }
}