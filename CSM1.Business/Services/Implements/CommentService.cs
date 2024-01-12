using AutoMapper;
using CSM1.Business.Dtos.CommentDtos;
using CSM1.Business.Dtos.TopicDtos;
using CSM1.Business.Repositories.Interfaces;
using CSM1.Business.Services.Interfaces;

namespace CSM1.Business.Services.Implements;

public class CommentService : ICommentService
{
    ITopicRepository _repo { get; }
    IMapper _mapper { get; }


    public CommentService(ITopicRepository repo,
        IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public IEnumerable<CommentListItemDto> GetAll()
    {
        return _mapper.Map<IEnumerable<CommentListItemDto>>(_repo.GetAll());
    }
}
