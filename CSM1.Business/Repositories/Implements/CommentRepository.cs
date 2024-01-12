using CSM1.Business.Repositories.Interfaces;
using CSM1.Core.Entities;
using CSM1.DAL.Contexts;

namespace CSM1.Business.Repositories.Implements;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(CSM1DbContext context) : base(context)
    {
    }
}
