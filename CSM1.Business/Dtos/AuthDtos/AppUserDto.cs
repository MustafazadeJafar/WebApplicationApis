using CSM1.Business.Exceptions.Auth;
using CSM1.Business.Exceptions.Roles;
using CSM1.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace CSM1.Business.Dtos.AuthDtos;

public class AppUserDto
{
    public static async Task<AppUserDto> Create(AppUser user, UserManager<AppUser> userManager)
    {
        return new AppUserDto()
        {
            UserName = user.UserName,
            Email = user.Email,
            MainRole = (await userManager.GetRolesAsync(user)).First(),
            Name = user.Name,
            Surname = user.Surname,
            BirthDay = user.BirthDay,
        };
    }
    public static async Task<AppUser> Create(AppUserDto dto, UserManager<AppUser> userManager)
    {
        AppUser user = new()
        {
            UserName = dto.UserName,
            Email = dto.Email,
            Name = dto.Name,
            Surname = dto.Surname,
            BirthDay = dto.BirthDay,
        };
        if (!(await userManager.CreateAsync(user, dto.Password)).Succeeded) throw new UsernameOrEmailExistException();
        if (!(await userManager.AddToRoleAsync(user, dto.MainRole)).Succeeded) throw new RoleAddException();
        return user;
    }

    public string UserName { get; set; }
    public string Email { get; set; }
    public string? Password { get; set; }


    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDay { get; set; }

    public string MainRole { get; set; }
    public string Fullname
    {
        get => this.Name + "_" + this.Surname;
    }
}
