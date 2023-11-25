using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.AdditionalLearning;

public class LearningStatusEntityConfiguration : IEntityTypeConfiguration<LearningStatus>
{
    public void Configure(EntityTypeBuilder<LearningStatus> builder)
    {
        builder.HasKey(property => property.LearningStatusId);

        builder.Property(property => property.LearningStatusName)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(property => property.Students)
            .WithOne(property => property.LearningStatus)
            .HasForeignKey(property => property.LearningStatusId)
            .IsRequired();
    }
}