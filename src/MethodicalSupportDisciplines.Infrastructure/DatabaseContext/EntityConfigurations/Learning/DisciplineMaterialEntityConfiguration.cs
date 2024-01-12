using MethodicalSupportDisciplines.Core.Entities.Learning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.Learning;

public class DisciplineMaterialEntityConfiguration : IEntityTypeConfiguration<DisciplineMaterial>
{
    public void Configure(EntityTypeBuilder<DisciplineMaterial> builder)
    {
        builder.HasKey(property => property.DisciplineMaterialId);

        builder.Property(property => property.DisciplineMaterialName)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(property => property.DisciplineMaterialDescription)
            .HasMaxLength(5000)
            .IsRequired();
        
        builder.Property(property => property.CreatedDate)
            .HasDefaultValue(DateTime.Now)
            .IsRequired();

        builder.HasOne(property => property.Discipline)
            .WithMany(property => property.DisciplineMaterials)
            .HasForeignKey(property => property.DisciplineId)
            .IsRequired();
        
        builder.HasOne(property => property.DisciplineMaterialType)
            .WithMany(property => property.DisciplineMaterials)
            .HasForeignKey(property => property.DisciplineMaterialTypeId)
            .IsRequired();
    }
}