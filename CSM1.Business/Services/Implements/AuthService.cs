using CSM1.Business.Dtos.AuthDtos;
using CSM1.Business.Exceptions.Auth;
using CSM1.Business.ExternalServices.Interfaces;
using CSM1.Business.Services.Interfaces;
using CSM1.Core.Entities;
using CSM1.Core.Entities.Static;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

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

    public void SendConfirmation(AppUserDto dto)
    {
        string body = "";
        bool isHtml = false;
        //using (StreamReader readtext = new StreamReader("template1.html"))
        //{
        //    body = readtext.ReadToEnd();
        //    isHtml = true;
        //}

        body += this._context.HttpContext?.Request.Scheme + "://" +
            this._context.HttpContext?.Request.Host.Value + "/api/Auth/Confirm?token=" +
            this._tokenService.CreateUserToken(dto).Token;

        throw new EmailNotConfirmedException(body); // this way email service skipped due to lazy author not wanting to create app password
        this._emailService.Send(dto.Email, "Welcome to club buddy", body, isHtml);
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
        if (!result.Succeeded) throw new UsernameOrEmailExistException();

        //result = 
        await this._userManager.AddToRoleAsync(user, nameof(Roles.AuthRoles.User));
        //if (!result.Succeeded) return false;

        this.SendConfirmation(await AppUserDto.Create(user, this._userManager));

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

        if (user == null || !(await this._userManager.CheckPasswordAsync(user, dto.Password)))
            throw new WrongUsernameOrPasswordException();
        if (!user.EmailConfirmed)
        {
            this.SendConfirmation(await AppUserDto.Create(user, this._userManager));
            throw new EmailNotConfirmedException();
        }

        return this._tokenService.CreateUserToken(await AppUserDto.Create(user, this._userManager));
    }

    public async Task CreateRoles()
    {
        foreach (string item in Enum.GetNames<Roles.AuthRoles>())
        {
            if (!await this._roleManager.RoleExistsAsync(item))
            {
                var result = await this._roleManager.CreateAsync(new IdentityRole
                {
                    Name = item
                });
                if (!result.Succeeded)
                {
                    return;
                }
            }
        }

        return;
    }

    public async Task<bool> ConfirmEmail(string token, bool skipValidation = true)
    {
        if (skipValidation || await this._tokenService.VakidateToken(token))
        {
            AppUser user = await this._userManager.FindByNameAsync(ITokenService.GetJwtClaim(token, "UserName"));
            if (!user.EmailConfirmed)
            {
                user.EmailConfirmed = true;
                await this._userManager.UpdateAsync(user);
            }
            return true;
        }
        else return false;
    }
}
