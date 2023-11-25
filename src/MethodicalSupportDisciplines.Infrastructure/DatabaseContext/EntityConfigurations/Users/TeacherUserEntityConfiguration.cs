using MethodicalSupportDisciplines.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.Users;

public class TeacherUserEntityConfiguration : IEntityTypeConfiguration<TeacherUser>
{
    public void Configure(EntityTypeBuilder<TeacherUser> builder)
    {
        builder.HasKey(property => property.TeacherUserId);
    }
}