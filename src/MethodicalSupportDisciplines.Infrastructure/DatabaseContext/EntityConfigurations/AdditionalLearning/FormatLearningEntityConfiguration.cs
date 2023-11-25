using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.AdditionalLearning;

public class FormatLearningEntityConfiguration : IEntityTypeConfiguration<FormatLearning>
{
    public void Configure(EntityTypeBuilder<FormatLearning> builder)
    {
        builder.HasKey(property => property.FormatLearningId);

        builder.Property(property => property.FormatLearningName)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(property => property.Students)
            .WithOne(property => property.FormatLearning)
            .HasForeignKey(property => property.FormatLearningId)
            .IsRequired();
    }
}