using AutoMapper;
using Gateway.Models.Entities;
using Gateway.Models.Post;
using Gateway.Models.Update;
using Gateway.Models.View;
using Gateway.Repository;

namespace Gateway.Logic;

public class SectionLogic : ISectionLogic
{
    private readonly ISectionRepository _sectionRepository;
    private readonly IMapper _mapper;

    public SectionLogic(ISectionRepository sectionRepository, IMapper mapper)
    {
        _sectionRepository = sectionRepository;
        _mapper = mapper;
    }

    public async Task<SectionViewDto> GetSectionAsync(Guid id)
        => _mapper.Map<SectionViewDto>(
            await _sectionRepository.GetSectionAsync(id).ConfigureAwait(false));

    public async Task<IEnumerable<SectionViewDto>> GetSectionsAsync()
        => _mapper.Map<IEnumerable<SectionViewDto>>(
            await _sectionRepository.GetSectionsAsync().ConfigureAwait(false));

    public async Task AddSectionAsync(SectionPostDto section)
        => await _sectionRepository.AddSectionAsync(
            _mapper.Map<Section>(section)).ConfigureAwait(false);

    public async Task DeleteSectionAsync(Guid id)
        => await _sectionRepository.RemoveSectionAsync(id).ConfigureAwait(false);

    public async Task UpdateSectionAsync(SectionUpdateDto section)
        => await _sectionRepository.UpdateSectionAsync(
            _mapper.Map<Section>(section)).ConfigureAwait(false);
}