using AutoMapper;
using Gateway.Core.Models;
using Gateway.Repository;

namespace Gateway.Logic
{
    public class LogicCrud<T, TView, TPost, TUpdate> : ILogicCrud<TView, TPost, TUpdate>
        where T : class
        where TView : class
        where TPost : class
        where TUpdate : class
    {
        private readonly IRepositoryCrud<T> _repository;
        private readonly IMapper _mapper;

        public LogicCrud(IRepositoryCrud<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<Guid> AddAsync(TPost obj)
            => await _repository.AddAsync(
                _mapper.Map<T>(obj))
                .ConfigureAwait(false);

        public virtual async Task<TView> GetAsync(Guid id)
            => _mapper.Map<TView>(
                await _repository.GetAsync(id)
                    .ConfigureAwait(false));

        public virtual async Task<DataWithSlicePaginationDto<TView>> GetAsync(QueriesRequestDto? options)
        {
            DataWithSlicePaginationDto<TView> result = new DataWithSlicePaginationDto<TView>()
            {
                Items = null,
                IsLast = true
            };

            if (options == null)
            {
                result.Items = _mapper.Map<IEnumerable<TView>>(
                    await _repository.GetAsync()
                        .ConfigureAwait(false))
                    .ToArray();
            }
            else if( options.Pattern != null)
            {
                result.Items = _mapper.Map<IEnumerable<TView>>(
                    await _repository.GetAsync(options.Pattern)
                        .ConfigureAwait(false))
                    .ToArray();
            }
            else
            {
                result.Items = _mapper.Map<IEnumerable<TView>>(
                    await _repository.GetAsync((int)options.Page, (int)options.PageSize)
                        .ConfigureAwait(false))
                    .ToArray();

                result.IsLast = (int)options.Page * (int)options.PageSize + (int)options.PageSize
                        >= (await _repository.GetCountAsync()
                            .ConfigureAwait(false));
            }

            return result;
        }

        public virtual async Task RemoveAsync(Guid id)
            => await _repository.RemoveAsync(id)
                .ConfigureAwait(false);

        public virtual async Task UpdateAsync(TUpdate obj)
            => await _repository.UpdateAsync(
                    _mapper.Map<T>(obj)
                ).ConfigureAwait(false);
    }
}