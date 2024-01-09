using CSM1.Business.Dtos.BlogDtos;
using CSM1.Business.Repositories.Interfaces;
using CSM1.Business.Services.Interfaces;

namespace CSM1.Business.Services.Implements;

public class BlogService : IBlogService
{
    IBlogRepository _rep { get; }

    public BlogService(IBlogRepository rep)
    {
        _rep = rep;
    }

    public IEnumerable<BlogDetailDto> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<BlogDetailDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(BlogCreateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, BlogDetailDto dto)
    {
        throw new NotImplementedException();
    }
}
