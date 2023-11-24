using MethodicalSupportDisciplines.BLL.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext;

public class DataDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public DataDbContext(DbContextOptions<DataDbContext> options)
        : base(options)
    {
    }
}