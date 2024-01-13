using CSM1.Business.Dtos.TopicDtos;
using CSM1.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSM1.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TopicsController : ControllerBase
{
    ITopicService _service { get; }

    public TopicsController(ITopicService service)
    {
        _service = service;
    }
    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        return Ok(_service.GetAll());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
    [HttpPost]
    public async Task<IActionResult> Post(TopicCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.RemoveAsync(id);
        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TopicUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return Ok();
    }
}
