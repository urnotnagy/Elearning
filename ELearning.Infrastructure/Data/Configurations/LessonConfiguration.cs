using ELearning.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearning.Infrastructure.Data.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(l => l.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(l => l.Content)
                .IsRequired();

            builder.Property(l => l.VideoUrl)
                .HasMaxLength(500);

            builder.Property(l => l.Order)
                .IsRequired();

            builder.Property(l => l.DurationInMinutes)
                .IsRequired();

            // Relationships
            builder.HasOne(l => l.Module)
                .WithMany(m => m.Lessons)
                .HasForeignKey(l => l.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(l => l.Resources)
                .WithOne(r => r.Lesson)
                .HasForeignKey(r => r.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(l => l.StudentProgress)
                .WithOne(p => p.Lesson)
                .HasForeignKey(p => p.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 
 