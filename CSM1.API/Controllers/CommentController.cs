using CSM1.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CSM1.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    ICommentService _service { get; }

    public CommentController(ICommentService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_service.GetAll());
    }
}
