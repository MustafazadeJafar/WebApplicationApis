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


}
