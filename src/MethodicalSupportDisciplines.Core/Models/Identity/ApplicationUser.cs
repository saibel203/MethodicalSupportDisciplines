using MethodicalSupportDisciplines.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace MethodicalSupportDisciplines.Core.Models.Identity;

public class ApplicationUser : IdentityUser
{
    public GuestUser? GuestUser { get; set; }
    public TeacherUser? TeacherUser { get; set; }
    public StudentUser? StudentUser { get; set; }
    public AdminUser? AdminUser { get; set; }
}