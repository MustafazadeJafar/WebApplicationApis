using CSM1.Business.Dtos.AuthDtos;

namespace CSM1.Business.ExternalServices.Interfaces;

public interface ITokenService
{
    public TokenDto CreateUserToken(AppUserDto user);
}
