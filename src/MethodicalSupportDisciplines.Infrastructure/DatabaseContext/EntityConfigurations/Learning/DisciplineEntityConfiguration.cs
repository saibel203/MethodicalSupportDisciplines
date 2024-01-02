using MethodicalSupportDisciplines.Core.Entities.Learning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.Learning;

public class DisciplineEntityConfiguration : IEntityTypeConfiguration<Discipline>
{
    public void Configure(EntityTypeBuilder<Discipline> builder)
    {
        builder.HasKey(property => property.DisciplineId);

        builder.Property(property => property.DisciplineName)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(property => property.DisciplineDescription)
            .HasMaxLength(5000)
            .IsRequired();

        builder.Property(property => property.DisciplineCreatedDate)
            .HasDefaultValue(DateTime.Now)
            .IsRequired();

        builder.HasOne(property => property.Teacher)
            .WithMany(property => property.Disciplines)
            .HasForeignKey(property => property.TeacherId)
            .IsRequired();
    }
}