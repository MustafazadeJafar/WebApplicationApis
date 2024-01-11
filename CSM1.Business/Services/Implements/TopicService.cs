using AutoMapper;
using CSM1.Business.Dtos.TopicDtos;
using CSM1.Business.Repositories.Interfaces;
using CSM1.Business.Services.Interfaces;
using CSM1.Business.Exceptions.Common;
using CSM1.Business.Exceptions.Topic;
using CSM1.Core.Entities;

namespace CSM1.Business.Services.Implements;

public class TopicService : ITopicService
{
    ITopicRepository _repo { get; }
    IMapper _mapper { get; }


    public TopicService(ITopicRepository repo,
        IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task CreateAsync(TopicCreateDto dto)
    {
        if (await _repo.IsExistAsync(r => r.Name.ToLower() == dto.Name.ToLower()))
            throw new EntityExistException();
        await _repo.CreateAsync(_mapper.Map<Topic>(dto));
        await _repo.SaveAsync();
    }

    public IEnumerable<TopicListItemDto> GetAll()
        => _mapper.Map<IEnumerable<TopicListItemDto>>(_repo.GetAll());

    public async Task<TopicDetailDto> GetByIdAsync(int id)
    {
        var data = await _checkId(id, true);
        return _mapper.Map<TopicDetailDto>(data);
    }

    public async Task RemoveAsync(int id, bool soft = true)
    {
        var data = await _checkId(id);
        _repo.Remove(data, soft);
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(int id, TopicUpdateDto dto)
    {
        var data = await _checkId(id);
        if (dto.Name.ToLower() != data.Name.ToLower())
        {
            if (await _repo.IsExistAsync(r => r.Name.ToLower() == dto.Name.ToLower()))
                throw new EntityExistException();
            data = _mapper.Map(dto, data);
            await _repo.SaveAsync();
        }
    }

    async Task<Topic> _checkId(int id, bool isTrack = false)
    {
        if (id <= 0) throw new ArgumentException();
        var data = await _repo.GetByIdAsync(id, isTrack);
        return data ?? throw new NotFoundException<Topic>();
    }
}
