using CustomApi.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomApi.DAL.Contexts;

public class CustomApiDbContext : IdentityDbContext<AppUser>
{

    public CustomApiDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<GameSession> GameSessions { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
}
