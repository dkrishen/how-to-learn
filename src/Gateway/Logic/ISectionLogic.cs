using Gateway.Models.Post;
using Gateway.Models.Update;
using Gateway.Models.View;

namespace Gateway.Logic;

public interface ISectionLogic : ILogicCrud<SectionViewDto, SectionPostDto, SectionUpdateDto> 
{
    public Task<IEnumerable<SectionViewDto>> GenerateResponseAsync(string request);
}