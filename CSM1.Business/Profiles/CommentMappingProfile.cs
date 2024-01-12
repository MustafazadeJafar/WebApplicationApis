using AutoMapper;
using CSM1.Business.Dtos.CommentDtos;
using CSM1.Core.Entities;

namespace CSM1.Business.Profiles;

public class CommentMappingProfile : Profile
{
    public CommentMappingProfile()
    {
        //CreateMap<TopicCreateDto, Topic>();
        //CreateMap<TopicUpdateDto, Topic>();
        CreateMap<Comment, CommentListItemDto>();
        //CreateMap<Topic, TopicDetailDto>();
    }
}
