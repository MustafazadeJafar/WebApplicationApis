using CSM1.Business.Dtos.AuthDtos;
using CSM1.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSM1.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    IAuthService _authService { get; }

    public AuthController(IAuthService authService)
    {
        this._authService = authService;
    }

    [HttpGet("Confirm")]
    public async Task<IActionResult> Get(string token)
    {
        if (await this._authService.ConfirmEmail(token)) 
        {
            return Ok("confirmed");
        }
        else 
        { 
            return BadRequest(); 
        }
    }

    // POST api/<AuthController>/Register
    [HttpPost("Register")]
    public async Task<string> Post(RegisterDto dto)
    {
        return ((await this._authService.Register(dto)).ToString());
    }

    // POST api/<AuthController>/Login
    [HttpPost("Login")]
    public async Task<TokenDto> Post(LoginDto dto)
    {
        return await this._authService.Login(dto);
    }

    [HttpGet("CreateRoles")]
    public async Task Post()
    {
        await this._authService.CreateRoles();
    }
}
