using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomApi.Core.Entities;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDay { get; set; }
    [NotMapped]
    public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
}
