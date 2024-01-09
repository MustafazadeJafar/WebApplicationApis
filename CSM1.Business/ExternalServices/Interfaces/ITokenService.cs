using CSM1.Business.Dtos.AuthDtos;
using CSM1.Business.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CSM1.Business.ExternalServices.Interfaces;

public interface ITokenService
{
    public TokenDto CreateUserToken(AppUserDto user);
    public Task<bool> VakidateToken(string token);
    public string GetClaim(string token, string type);
    
    public static TokenValidationParameters JwtTokenValidationParametrs(JwtTokenParameters parameters)
    {
        return new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = parameters.Issuer,
            ValidAudience = parameters.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(parameters.Salt)),
            LifetimeValidator = (nb, exp, token, _) => token != null && exp > DateTime.UtcNow && nb < DateTime.UtcNow,
        };
    }
}
