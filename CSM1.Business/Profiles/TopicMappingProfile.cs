using AutoMapper;
using CSM1.Business.Dtos.TopicDtos;
using CSM1.Core.Entities;

namespace CSM1.Business.Profiles;

public class TopicMappingProfile : Profile
{
    public TopicMappingProfile()
    {
        CreateMap<TopicCreateDto, Topic>();
        CreateMap<TopicUpdateDto, Topic>();
        CreateMap<Topic, TopicListItemDto>();
        CreateMap<Topic, TopicDetailDto>();
    }
}
