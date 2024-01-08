using MethodicalSupportDisciplines.Core.Entities.Learning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.Learning;

public class MaterialEntityConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.HasKey(property => property.MaterialId);

        builder.Property(property => property.MaterialPath)
            .IsRequired();

        builder.HasOne(property => property.MaterialType)
            .WithMany(property => property.Materials)
            .HasForeignKey(property => property.MaterialTypeId)
            .IsRequired();
    }
}