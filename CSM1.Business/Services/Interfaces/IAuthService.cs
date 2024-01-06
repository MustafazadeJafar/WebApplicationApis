using CSM1.Business.Dtos.AuthDtos;
using Microsoft.AspNetCore.Identity;

namespace CSM1.Business.Services.Interfaces;

public interface IAuthService
{
    public Task<IdentityResult> Register(RegisterDto dto);
    public Task<string> Login(LoginDto dto);
    public Task<bool> ConfirmEmail(string token);
    public Task Logout();
    public Task CreateRoles();

    public enum AuthRoles
    {
        User,
        Admin,
        SuperAdmin,
    }
}
