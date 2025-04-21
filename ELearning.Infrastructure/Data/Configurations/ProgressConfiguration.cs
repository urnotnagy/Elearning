using ELearning.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearning.Infrastructure.Data.Configurations
{
    public class ProgressConfiguration : IEntityTypeConfiguration<Progress>
    {
        public void Configure(EntityTypeBuilder<Progress> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.TimeSpent)
                .IsRequired();

            builder.Property(p => p.IsCompleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne(p => p.Student)
                .WithMany(u => u.LessonProgress)
                .HasForeignKey(p => p.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Lesson)
                .WithMany(l => l.StudentProgress)
                .HasForeignKey(p => p.LessonId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 