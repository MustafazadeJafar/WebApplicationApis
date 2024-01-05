using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using CSM1.Core.Entities;
using CSM1.Core.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CSM1.DAL.Contexts;

public class CSM1DbContext : IdentityDbContext<AppUser>
{
    public CSM1DbContext(DbContextOptions opt) : base(opt)
    {
    }

    // //
    public DbSet<Topic> Topics { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }

    // //
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
                entry.Entity.CreatedTime = DateTime.UtcNow;
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfigurationsFromAssembly(typeof(TopicConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
