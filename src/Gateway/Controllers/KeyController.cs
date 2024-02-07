using Gateway.Logic;
using Gateway.Models.Post;
using Gateway.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KeyController : ControllerBase
{
    private readonly IKeyLogic _keyLogic;

    public KeyController(IKeyLogic keyLogic)
    {
        _keyLogic = keyLogic;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _keyLogic.GetAsync(id).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _keyLogic.GetAsync().ConfigureAwait(false);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] KeyPostDto key)
    {
        await _keyLogic.AddAsync(key).ConfigureAwait(false);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] Guid id)
    {
        await _keyLogic.RemoveAsync(id).ConfigureAwait(false);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] KeyUpdateDto key)
    {
        await _keyLogic.UpdateAsync(key).ConfigureAwait(false);
        return Ok();
    }
}