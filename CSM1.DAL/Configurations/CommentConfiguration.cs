using CSM1.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSM1.DAL.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(t => t.Content)
            .IsRequired()
            .HasMaxLength(160);

        builder.HasOne(e => e.Parent)              // Specifies that each employee has one manager.
            .WithMany(e => e.Childs)       // Specifies that each manager can have many Subordinates
            .HasForeignKey(e => e.ParentId)     // Specifies the foreign key property for this relationship.
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.AppUser)
            .WithMany(e => e.Comments)
            .HasForeignKey(e => e.AppUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.Blog)
            .WithMany(e => e.Comments)
            .HasForeignKey(e => e.BlogId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
