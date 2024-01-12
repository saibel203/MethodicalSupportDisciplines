using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.AdditionalLearning;

public class SpecialtyEntityConfiguration : IEntityTypeConfiguration<Speciality>
{
    public void Configure(EntityTypeBuilder<Speciality> builder)
    {
        builder.HasKey(property => property.SpecialityId);

        builder.Property(property => property.SpecialityName)
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(property => property.SpecialityNumber)
            .IsRequired();

        builder.HasMany(property => property.Students)
            .WithOne(property => property.Speciality)
            .HasForeignKey(property => property.SpecialtyId)
            .IsRequired();
    }
}