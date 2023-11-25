using MethodicalSupportDisciplines.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.Users;

public class StudentUserEntityConfiguration : IEntityTypeConfiguration<StudentUser>
{
    public void Configure(EntityTypeBuilder<StudentUser> builder)
    {
        builder.HasKey(property => property.StudentUserId);

        builder.Property(property => property.PhoneNumber)
            .HasMaxLength(50)
            .IsRequired();
    }
}