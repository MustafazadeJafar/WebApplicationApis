using CSM1.Business.Dtos.AuthDtos;
using CSM1.Business.Extensions;
using CSM1.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSM1.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    //SignInManager<AppUser> _signInManager { get; }
    //UserManager<AppUser> _userManager { get; }
    //RoleManager<IdentityRole> _roleManager { get; }
    //IEmailService _emailService { get; }

    IAuthService _authService { get; }

    public AuthController(
        //SignInManager<AppUser> signInManager,
        //UserManager<AppUser> userManager,
        //RoleManager<IdentityRole> roleManager,
        //IEmailService emailService,
        IAuthService authService)
    {
        //this._signInManager = signInManager;
        //this._userManager = userManager;
        //this._roleManager = roleManager;
        //this._emailService = emailService;

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
        StaticHolderExtension.Hosting = HttpContext.Request.Host.Value;
        return ((await this._authService.Register(dto)).Succeeded.ToString());
    }

    // POST api/<AuthController>/Login
    [HttpPost("Login")]
    public async Task<string> Post(LoginDto dto)
    {
        return await this._authService.Login(dto);
    }

    [HttpGet("CreateRoles")]
    public async Task Get()
    {
        await this._authService.CreateRoles();
    }
}
