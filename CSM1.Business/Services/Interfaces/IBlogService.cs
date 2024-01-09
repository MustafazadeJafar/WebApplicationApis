using CSM1.Business.Dtos.BlogDtos;

namespace CSM1.Business.Services.Interfaces;

public interface IBlogService
{
    public IEnumerable<BlogDetailDto> GetAll();
    public Task<BlogDetailDto> GetByIdAsync(int id);
    public Task CreateAsync(BlogCreateDto dto);
    public Task RemoveAsync(int id);
    public Task UpdateAsync(int id, BlogDetailDto dto);
}
