using MethodicalSupportDisciplines.Core.Entities.Learning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.Learning;

public class MaterialDisciplineMaterialEntityConfiguration : IEntityTypeConfiguration<MaterialDisciplineMaterial>
{
    public void Configure(EntityTypeBuilder<MaterialDisciplineMaterial> builder)
    {
        builder.HasKey(property => new { property.MaterialId, property.DisciplineMaterialId });

        builder.HasOne<Material>(property => property.Material)
            .WithMany(property => property.Materials)
            .HasForeignKey(property => property.MaterialId);
        
        builder.HasOne<DisciplineMaterial>(property => property.DisciplineMaterial)
            .WithMany(property => property.Materials)
            .HasForeignKey(property => property.DisciplineMaterialId);
    }
}