using CSM1.Business.Dtos.AuthDtos;
using CSM1.Business.ExternalServices.Interfaces;
using CSM1.Business.Services.Interfaces;
using CSM1.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CSM1.Core.Entities.Static;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using CSM1.Business.Exceptions.Auth;

namespace CSM1.Business.Services.Implements;

public class AuthService : IAuthService
{
    AppUser _universal;
    UserManager<AppUser> _userManager { get; }
    RoleManager<IdentityRole> _roleManager { get; }
    ITokenService _tokenService { get; }
    IEmailService _emailService { get; }
    IHttpContextAccessor _context { get; }

    public AuthService(UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IEmailService emailService,
        IHttpContextAccessor context,
        ITokenService tokenService)
    {
        this._userManager = userManager;
        this._roleManager = roleManager;
        this._emailService = emailService;
        this._context = context;
        this._tokenService = tokenService;
    }

    public async Task<bool> Register(RegisterDto dto)
    {
        var user = new AppUser
        {
            Name = dto.Name,
            Surname = dto.Surname,
            Email = dto.Email,
            UserName = dto.Username,
        };
        var result = await this._userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) throw new UsernameExistException();

        //result = 
            await this._userManager.AddToRoleAsync(user, nameof(Roles.AuthRoles.User));
        //if (!result.Succeeded) return false;

        string body = "";
        bool isHtml = false;
        //using (StreamReader readtext = new StreamReader("template1.html"))
        //{
        //    body = readtext.ReadToEnd();
        //}
        //isHtml = true;
        body += this._context.HttpContext.Request.Scheme + this._context.HttpContext.Request.Host.Value +
            "/api/Auth/Confirm?token=" + this._tokenService.CreateUserToken(new AppUserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                MainRole = (await this._userManager.GetRolesAsync(user))[0],
                Name = user.Name,
                Surname = user.Surname,
                BirthDay = user.BirthDay,
            }).Token;

        //this._emailService.Send(user.Email, "Welcome to club buddy", body, isHtml);
        throw new UsernameExistException(body);
        return true;
    }

    public async Task<TokenDto> Login(LoginDto dto)
    {
        AppUser user;

        if (dto.UsernameOrEmail.Contains('@'))
        {
            user = await this._userManager.FindByEmailAsync(dto.UsernameOrEmail);
        }
        else
        {
            user = await this._userManager.FindByNameAsync(dto.UsernameOrEmail);
        }

        if (user == null) throw new UsernameExistException();

        if (await this._userManager.CheckPasswordAsync(user, dto.Password))
        {
            return this._tokenService.CreateUserToken(new AppUserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                MainRole = (await this._userManager.GetRolesAsync(user))[0],
                Name = user.Name,
                Surname = user.Surname,
                BirthDay = user.BirthDay,
            });
        }
        else throw new UsernameExistException();
    }

    //[Authorize(Roles = "Admin, SuperAdmin")] ?
    public async Task CreateRoles()
    {
        //if (!await this.TokenCheck(token)) return;
        //if (!(await this._userManager.GetRolesAsync(this._universal)).
        //        Any(r => r == nameof(Roles.AuthRoles.Admin) || r == nameof(Roles.AuthRoles.SuperAdmin))) return;

        foreach (var item in Enum.GetValues<Roles.AuthRoles>())
        {
            if (!await this._roleManager.RoleExistsAsync(item.ToString()))
            {
                var result = await this._roleManager.CreateAsync(new IdentityRole
                {
                    Name = item.ToString()
                });
                if (!result.Succeeded)
                {
                    return;
                }
            }
        }

        return;
    }

    async Task<bool> TokenCheck(string token)
    {
        //_universal = await this._userManager.Users.FirstAsync(u => u.Token == token && u.TokensExpr > DateTime.Now);
        return _universal != null;
    }

    public async Task<bool> ConfirmEmail(string token)
    {
        if (await TokenCheck(token))
        {
            _universal.EmailConfirmed = true;
            await this._userManager.UpdateAsync(_universal);
            return true;
        }
        else
        {
            return false;
        }
    }
}
