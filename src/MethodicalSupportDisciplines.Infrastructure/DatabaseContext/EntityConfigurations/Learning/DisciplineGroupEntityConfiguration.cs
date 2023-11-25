using MethodicalSupportDisciplines.Core.Entities.Learning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.Learning;

public class DisciplineGroupEntityConfiguration : IEntityTypeConfiguration<DisciplineGroup>
{
    public void Configure(EntityTypeBuilder<DisciplineGroup> builder)
    {
        builder.HasKey(property => new { property.DisciplineId, property.GroupId });

        builder.HasOne<Discipline>(property => property.Discipline)
            .WithMany(property => property.DisciplineGroups)
            .HasForeignKey(property => property.DisciplineId);
        
        builder.HasOne<Group>(property => property.Group)
            .WithMany(property => property.DisciplineGroups)
            .HasForeignKey(property => property.GroupId);
    }
}