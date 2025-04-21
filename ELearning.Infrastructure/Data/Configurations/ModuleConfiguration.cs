using ELearning.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearning.Infrastructure.Data.Configurations
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(m => m.Order)
                .IsRequired();

            // Relationships
            builder.HasOne(m => m.Course)
                .WithMany(c => c.Modules)
                .HasForeignKey(m => m.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.Lessons)
                .WithOne(l => l.Module)
                .HasForeignKey(l => l.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.Quizzes)
                .WithOne(q => q.Module)
                .HasForeignKey(q => q.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.Assignments)
                .WithOne(a => a.Module)
                .HasForeignKey(a => a.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 