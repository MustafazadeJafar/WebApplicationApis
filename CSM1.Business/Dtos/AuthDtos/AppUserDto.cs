namespace CSM1.Business.Dtos.AuthDtos;

public class AppUserDto
{
    public string UserName { get; set; }
    public string Email { get; set; }


    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDay { get; set; }

    public string MainRole { get; set; }
    public string Fullname
    {
        get => this.Name + "_" + this.Surname;
    }
}
