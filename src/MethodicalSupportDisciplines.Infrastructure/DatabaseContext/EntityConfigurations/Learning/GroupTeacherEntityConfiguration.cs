using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.Learning;

public class GroupTeacherEntityConfiguration : IEntityTypeConfiguration<GroupTeacher>
{
    public void Configure(EntityTypeBuilder<GroupTeacher> builder)
    {
        builder.HasKey(property => new { property.TeacherId, property.GroupId });

        builder.HasOne<Group>(property => property.Group)
            .WithMany(property => property.GroupTeachers)
            .HasForeignKey(property => property.GroupId);
        
        builder.HasOne<TeacherUser>(property => property.TeacherUser)
            .WithMany(property => property.GroupTeachers)
            .HasForeignKey(property => property.TeacherId);
    }
}