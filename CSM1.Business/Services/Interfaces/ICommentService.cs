using CSM1.Business.Dtos.CommentDtos;
using CSM1.Business.Dtos.TopicDtos;

namespace CSM1.Business.Services.Interfaces;

public interface ICommentService
{
    public IEnumerable<CommentListItemDto> GetAll();
    //public Task<> GetByIdAsync(int id);
    //public Task CreateAsync( dto);
    //public Task RemoveAsync(int id, bool soft = true);
    //public Task UpdateAsync(int id,  dto);
}
