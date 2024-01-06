using CSM1.Business.Dtos.AuthDtos;
using CSM1.Business.Extensions;
using CSM1.Business.ExternalServices.Interfaces;
using CSM1.Business.Services.Interfaces;
using CSM1.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CSM1.Business.Services.Implements;

public class AuthService : IAuthService
{
    AppUser _universal;
    SignInManager<AppUser> _signInManager { get; }
    UserManager<AppUser> _userManager { get; }
    RoleManager<IdentityRole> _roleManager { get; }
    IEmailService _emailService { get; }

    public AuthService(SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IEmailService emailService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _emailService = emailService;
    }

    TokenDto CreateToken()
    {
        string charset = "abcdefghijklmnopqrstuvwxyz0123456789";
        string token = "";

        for (int i = 0; i < 255; i++)
        {
            token += charset[new Random().Next(charset.Length)];
        }

        return new TokenDto() { Token = token, TokensExpr = DateTime.Now.AddMonths(3), };
    }

    public async Task<IdentityResult> Register(RegisterDto dto)
    {
        TokenDto token = CreateToken();
        var user = new AppUser
        {
            Name = dto.Name,
            Surname = dto.Surname,
            Email = dto.Email,
            UserName = dto.Username,
            Token = token.Token,
            TokensExpr = (DateTime)token.TokensExpr,
        };
        var result = await this._userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) return result;

        var roleResult = await this._userManager.AddToRoleAsync(user, IAuthService.AuthRoles.User.ToString());
        if (!roleResult.Succeeded) return roleResult;

        string body = "";
        bool isHtml = false;
        //using (StreamReader readtext = new StreamReader("template1.html"))
        //{
        //    body = readtext.ReadToEnd();
        //}
        //isHtml = true;
        body += "http://" + StaticHolderExtension.Hosting + "/api/Auth/Confirm?token=" + token.Token;

        this._emailService.Send(user.Email, "Welcome to club buddy", body, isHtml);

        return roleResult;
    }

    public async Task<string> Login(LoginDto dto)
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
        if (user == null) return null;

        TokenDto token = this.CreateToken();
        user.Token = token.Token;
        user.TokensExpr = (DateTime)token.TokensExpr;

        var result = await this._signInManager.PasswordSignInAsync(user, dto.Password, dto.IsRemember, true);

        return result.Succeeded ? token.Token : "_";
    }

    public async Task Logout()
    {
        // is there any purpose for it?
        await this._signInManager.SignOutAsync();
    }

    public async Task CreateRoles()
    {
        foreach (var item in Enum.GetValues<IAuthService.AuthRoles>())
        {
            if (!await this._roleManager.RoleExistsAsync(item.ToString()))
            {
                var result = await this._roleManager.CreateAsync(new IdentityRole
                {
                    Name = item.ToString()
                });
                if (!result.Succeeded)
                {
                    return ;
                }
            }
        }
        return ;
    }

    async Task<bool> TokenCheck(string token)
    {
        _universal = await this._userManager.Users.FirstAsync(u => u.Token == token && u.TokensExpr > DateTime.Now);
        return _universal != null;
    }

    public async Task<bool> ConfirmEmail(string token)
    {
        if(await TokenCheck(token))
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
