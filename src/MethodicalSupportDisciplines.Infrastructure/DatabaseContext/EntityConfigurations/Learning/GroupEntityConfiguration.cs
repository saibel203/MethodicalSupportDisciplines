using MethodicalSupportDisciplines.Core.Entities.Learning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.Learning;

public class GroupEntityConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(property => property.GroupId);
        
        builder.Property(property => property.GroupName)
            .HasMaxLength(10)
            .IsRequired();

        builder.HasMany(property => property.StudentUsers)
            .WithOne(property => property.Group)
            .HasForeignKey(property => property.GroupId)
            .IsRequired();
    }
}