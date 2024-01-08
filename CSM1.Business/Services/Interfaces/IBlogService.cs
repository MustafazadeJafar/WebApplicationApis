using CSM1.Business.Dtos.BlogDtos;

namespace CSM1.Business.Services.Interfaces;

public interface IBlogService
{
    public IEnumerable<BlogListItemDto> GetAll();
    public Task<> GetByIdAsync(int id);
    public Task CreateAsync(BlogCreateDto dto);
    public Task RemoveAsync(int id);
    public Task UpdateAsync(int id,  dto);
}
