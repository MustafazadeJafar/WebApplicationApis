using CSM1.Business.Dtos.TopicDtos;

namespace CSM1.Business.Services.Interfaces;

public interface ITopicService
{
    public IEnumerable<TopicListItemDto> GetAll();
    public Task<TopicDetailDto> GetByIdAsync(int id);
    public Task CreateAsync(TopicCreateDto dto);
    public Task RemoveAsync(int id, bool soft = true);
    public Task UpdateAsync(int id, TopicUpdateDto dto);
}
