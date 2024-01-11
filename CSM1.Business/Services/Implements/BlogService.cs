using AutoMapper;
using CSM1.Business.Dtos.BlogDtos;
using CSM1.Business.Dtos.TopicDtos;
using CSM1.Business.Exceptions.Common;
using CSM1.Business.Repositories.Interfaces;
using CSM1.Business.Services.Interfaces;
using CSM1.Core.Entities;

namespace CSM1.Business.Services.Implements;

public class BlogService : IBlogService
{
    IBlogRepository _repo { get; }
    public Mapper MyProperty { get; set; }

    public BlogService(IBlogRepository repo,
        IMapper mapper)
    {

        _repo = repo;
    }

    public async Task CreateAsync(BlogCreateDto dto)
    {
        if (await _repo.IsExistAsync(r => r.Name.ToLower() == dto.Name.ToLower()))
            throw new EntityExistException();
        await _repo.CreateAsync(_mapper.Map<Topic>(dto));
        await _repo.SaveAsync();
    }

    public IEnumerable<BlogDetailDto> GetAll(params string[] includes)
        => _mapper.Map<IEnumerable<BlogListItemDto>>(_repo.GetAll());

    public async Task<BlogDetailDto> GetByIdAsync(int id, params string[] includes)
    {
        var data = await _checkId(id, true);
        return _mapper.Map<BlogDetailDto>(data);
    }

    public async Task RemoveAsync(int id, bool soft = true)
    {
        var data = await _checkId(id);
        _repo.Remove(data, soft);
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(int id, BlogDetailDto dto)
    {
        var data = await _checkId(id);
        if (dto.Name.ToLower() != data.Name.ToLower())
        {
            if (await _repo.IsExistAsync(r => r.Name.ToLower() == dto.Name.ToLower()))
                throw new EntityExistException<Blog>();
            data = _mapper.Map(dto, data);
            await _repo.SaveAsync();
        }
    }

    async Task<Blog> _checkId(int id, bool isTrack = false)
    {
        if (id <= 0) throw new ArgumentException();
        var data = await _repo.GetByIdAsync(id, isTrack);
        return data ?? throw new NotFoundException<Blog>();
    }
}
