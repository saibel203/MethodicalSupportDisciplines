using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.AdditionalLearning;

public class QualificationEntityConfiguration : IEntityTypeConfiguration<Qualification>
{
    public void Configure(EntityTypeBuilder<Qualification> builder)
    {
        builder.HasKey(property => property.QualificationId);

        builder.Property(property => property.QualificationName)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(property => property.Teachers)
            .WithOne(property => property.Qualification)
            .HasForeignKey(property => property.QualificationId)
            .IsRequired();
    }
}