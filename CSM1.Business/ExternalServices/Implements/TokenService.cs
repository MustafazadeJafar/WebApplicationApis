using CSM1.Business.Dtos.AuthDtos;
using CSM1.Business.ExternalServices.Interfaces;
using CSM1.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CSM1.Business.ExternalServices.Implements;

public class TokenService : ITokenService
{
    public TokenDto CreateUserToken(AppUserDto user)
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new(ClaimTypes.Name, user.UserName));
        claims.Add(new(ClaimTypes.GivenName, user.Fullname));
        claims.Add(new(ClaimTypes.Role, user.MainRole));
        claims.Add(new("BirthDay", user.BirthDay.ToString()));

        DateTime expires = DateTime.UtcNow.AddMinutes(3);

        SymmetricSecurityKey ssk = new(Encoding.UTF8.GetBytes("f08567d7-cef3-4781-ab31-032fcc87bf16"));
        SigningCredentials sc = new(ssk, SecurityAlgorithms.HmacSha256Signature);
        JwtSecurityToken jst = new("h", "h.api", claims, DateTime.UtcNow, expires, sc);

        return new TokenDto()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jst),
            Expires = expires,
        };
    }
}
