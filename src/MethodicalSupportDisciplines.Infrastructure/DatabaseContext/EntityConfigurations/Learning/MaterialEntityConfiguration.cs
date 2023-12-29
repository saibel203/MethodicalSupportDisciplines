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
            .IsRequired(false);
        
        builder.Property(property => property.MaterialBook)
            .IsRequired(false);
        
        builder.Property(property => property.MaterialUrl)
            .IsRequired(false);
    }
}