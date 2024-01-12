using CSM1.Business.Dtos.BlogDtos;

namespace CSM1.Business.Services.Interfaces;

public interface IBlogService
{
    public IEnumerable<BlogListItemDto> GetAll(params string[] includes);
    public Task<BlogDetailDto> GetByIdAsync(int id, params string[] includes);
    public Task CreateAsync(BlogCreateDto dto);
    public Task RemoveAsync(int id, bool soft = true);
    public Task UpdateAsync(int id, BlogDetailDto dto);
}
