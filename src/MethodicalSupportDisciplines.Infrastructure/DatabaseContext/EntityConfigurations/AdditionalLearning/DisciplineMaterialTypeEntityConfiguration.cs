using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.AdditionalLearning;

public class DisciplineMaterialTypeEntityConfiguration : IEntityTypeConfiguration<DisciplineMaterialType>
{
    public void Configure(EntityTypeBuilder<DisciplineMaterialType> builder)
    {
        builder.HasKey(property => property.DisciplineMaterialTypeId);

        builder.Property(property => property.DisciplineMaterialTypeName)
            .HasMaxLength(255)
            .IsRequired();
    }
}