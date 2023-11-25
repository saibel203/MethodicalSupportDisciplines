using MethodicalSupportDisciplines.Core.Entities.Users;
using MethodicalSupportDisciplines.Core.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.Users;

public class UserEntitiesConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasOne(applicationUser => applicationUser.GuestUser)
            .WithOne(guestUser => guestUser.ApplicationUser)
            .HasForeignKey<GuestUser>(guestUser => guestUser.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(applicationUser => applicationUser.StudentUser)
            .WithOne(studentUser => studentUser.ApplicationUser)
            .HasForeignKey<StudentUser>(guestUser => guestUser.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(applicationUser => applicationUser.TeacherUser)
            .WithOne(teacherUser => teacherUser.ApplicationUser)
            .HasForeignKey<TeacherUser>(guestUser => guestUser.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(applicationUser => applicationUser.AdminUser)
            .WithOne(adminUser => adminUser.ApplicationUser)
            .HasForeignKey<AdminUser>(guestUser => guestUser.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}