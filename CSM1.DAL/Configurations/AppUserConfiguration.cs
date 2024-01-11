using CSM1.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CSM1.DAL.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(32);
        builder.Property(b => b.BirthDay)
            .IsRequired()
            .HasColumnType("date");
    }
}
