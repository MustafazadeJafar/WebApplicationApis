﻿using Microsoft.AspNetCore.Identity;

namespace CSM1.Core.Entities;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDay { get; set; }
}
