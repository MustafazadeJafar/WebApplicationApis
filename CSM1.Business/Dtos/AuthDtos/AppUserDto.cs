using CSM1.Core.Entities;

namespace CSM1.Business.Dtos.AuthDtos;

public class AppUserDto
{
    public AppUserDto() { }
    public AppUserDto(AppUser user, string role)
    {
        UserName = user.UserName;
        Email = user.Email;
        MainRole = role;
        Name = user.Name;
        Surname = user.Surname;
        BirthDay = user.BirthDay;
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
