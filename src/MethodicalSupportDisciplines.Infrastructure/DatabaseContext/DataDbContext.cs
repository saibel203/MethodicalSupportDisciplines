using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Core.Entities.Users;
using MethodicalSupportDisciplines.Core.Models.Identity;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.AdditionalLearning;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.Learning;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext;

public class DataDbContext(DbContextOptions<DataDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
{
    // Users DbSets
    public DbSet<GuestUser> GuestUsers => Set<GuestUser>();
    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();
    public DbSet<StudentUser> StudentUsers => Set<StudentUser>();
    public DbSet<TeacherUser> TeacherUsers => Set<TeacherUser>();

    // Learning DbSets
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<GroupTeacher> GroupTeachers => Set<GroupTeacher>();
    public DbSet<Discipline> Disciplines => Set<Discipline>();
    public DbSet<DisciplineGroup> DisciplineGroups => Set<DisciplineGroup>();
    public DbSet<DisciplineMaterial> DisciplineMaterials => Set<DisciplineMaterial>();
    public DbSet<Material> Materials => Set<Material>();
    public DbSet<MaterialDisciplineMaterial> MaterialDisciplineMaterials => Set<MaterialDisciplineMaterial>();
    public DbSet<Mark> Marks => Set<Mark>();

    // Additional DbSets
    public DbSet<FormatLearning> FormatLearnings => Set<FormatLearning>();
    public DbSet<LearningStatus> LearningStatuses => Set<LearningStatus>();
    public DbSet<Faculty> Faculties => Set<Faculty>();
    public DbSet<Speciality> Specialties => Set<Speciality>();
    public DbSet<Qualification> Qualifications => Set<Qualification>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Users configurations
        builder.ApplyConfigurationsFromAssembly(typeof(UserEntitiesConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(GuestUserEntityConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(StudentUserEntityConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(TeacherUserEntityConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(AdminUserEntityConfiguration).Assembly);

        // Learning configurations
        builder.ApplyConfigurationsFromAssembly(typeof(GroupEntityConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(GroupTeacherEntityConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(DisciplineEntityConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(DisciplineGroupEntityConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(MarkEntityConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(DisciplineMaterialEntityConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(MaterialEntityConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(MaterialDisciplineMaterial).Assembly);

        // Additional Learning Data configurations
        builder.ApplyConfigurationsFromAssembly(typeof(FormatLearningEntityConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(LearningStatusEntityConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(FacultyEntityConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(SpecialtyEntityConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(QualificationEntityConfiguration).Assembly);
    }
}