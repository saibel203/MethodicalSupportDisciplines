using MethodicalSupportDisciplines.Core.Entities.Users;
using MethodicalSupportDisciplines.Core.Models.Identity;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext;

public class DataDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public DataDbContext(DbContextOptions<DataDbContext> options)
        : base(options)
    {
    }

    public DbSet<GuestUser> GuestUsers => Set<GuestUser>();
    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();
    public DbSet<StudentUser> StudentUsers => Set<StudentUser>();
    public DbSet<TeacherUser> TeacherUsers => Set<TeacherUser>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Users configurations
        builder.ApplyConfigurationsFromAssembly(typeof(UserEntitiesConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(GuestUserEntityConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(StudentUserEntityConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(TeacherUserEntityConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(AdminUserEntityConfiguration).Assembly);
    }
}