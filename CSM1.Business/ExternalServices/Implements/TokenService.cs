using CSM1.Business.Dtos.AuthDtos;
using CSM1.Business.ExternalServices.Interfaces;
using CSM1.Business.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CSM1.Business.ExternalServices.Implements;

public class TokenService : ITokenService
{
    JwtTokenParameters _parameters { get; }

    public TokenService(IConfiguration configuration)
    {
        this._parameters = configuration.GetSection("Jwt").Get<JwtTokenParameters>();
    }

    public TokenDto CreateUserToken(AppUserDto user)
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new(ClaimTypes.GivenName, user.Fullname));
        claims.Add(new("MainRole", user.MainRole));
        claims.Add(new("UserName", user.UserName));
        claims.Add(new("BirthDay", user.BirthDay.ToString()));

        DateTime expires = DateTime.UtcNow.AddMinutes(this._parameters.ExpireMinutes);

        SymmetricSecurityKey ssk = new(Encoding.UTF8.GetBytes(this._parameters.Salt));
        SigningCredentials sc = new(ssk, SecurityAlgorithms.HmacSha256Signature);
        JwtSecurityToken jst = new(this._parameters.Issuer, this._parameters.Audience, claims, DateTime.UtcNow, expires, sc);

        return new TokenDto()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jst),
            Expires = expires,
        };
    }

    public async Task<bool> VakidateToken(string token)
    {
        JwtSecurityTokenHandler handler = new();
        var result = await handler.ValidateTokenAsync(token, ITokenService.JwtTokenValidationParametrs(this._parameters));
        return result.IsValid;
    }
}
