using MethodicalSupportDisciplines.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.Users;

public class StudentUserEntityConfiguration : IEntityTypeConfiguration<StudentUser>
{
    public void Configure(EntityTypeBuilder<StudentUser> builder)
    {
        builder.HasKey(property => property.StudentUserId);
        
        builder.Property(property => property.FirstName)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(property => property.LastName)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(property => property.Patronymic)
            .HasMaxLength(50)
            .IsRequired();
    }
}