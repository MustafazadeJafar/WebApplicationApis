using CustomApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomApi.DAL.Contexts;

public class CustomApiDbContext : DbContext
{

    public CustomApiDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<GameSession> GameSessions { get; set; }
}
