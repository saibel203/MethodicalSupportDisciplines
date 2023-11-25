using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.AdditionalLearning;

public class FacultyEntityConfiguration : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> builder)
    {
        builder.HasKey(property => property.FacultyId);

        builder.Property(property => property.FacultyName)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(property => property.FacultyShortName)
            .HasMaxLength(15)
            .IsRequired();

        builder.HasMany(property => property.Students)
            .WithOne(property => property.Faculty)
            .HasForeignKey(property => property.FacultyId)
            .IsRequired();
    }
}