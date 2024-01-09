using CSM1.Business.Dtos.AuthDtos;
using Microsoft.AspNetCore.Identity;

namespace CSM1.Business.Services.Interfaces;

public interface IAuthService
{
    public Task<bool> Register(RegisterDto dto);
    public Task<TokenDto> Login(LoginDto dto);
    public Task<bool> ConfirmEmail(string token);
    public Task CreateRoles();
}
