using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.AdditionalLearning;

public class SpecialtyEntityConfiguration : IEntityTypeConfiguration<Specialty>
{
    public void Configure(EntityTypeBuilder<Specialty> builder)
    {
        builder.HasKey(property => property.SpecialtyId);

        builder.Property(property => property.SpecialtyName)
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(property => property.SpecialityNumber)
            .IsRequired();

        builder.HasMany(property => property.Students)
            .WithOne(property => property.Specialty)
            .HasForeignKey(property => property.SpecialtyId)
            .IsRequired();
    }
}