using MethodicalSupportDisciplines.Core.Entities.Learning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.Learning;

public class MarkEntityConfiguration : IEntityTypeConfiguration<Mark>
{
    public void Configure(EntityTypeBuilder<Mark> builder)
    {
        builder.HasKey(property => property.MarkId);

        builder.Property(property => property.MarkValue)
            .IsRequired();

        builder.HasOne(property => property.Student)
            .WithMany(property => property.Marks)
            .HasForeignKey(property => property.StudentId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
        
        builder.HasOne(property => property.Teacher)
            .WithMany(property => property.Marks)
            .HasForeignKey(property => property.TeacherId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
        
        builder.HasOne(property => property.Discipline)
            .WithMany(property => property.Marks)
            .HasForeignKey(property => property.DisciplineId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}