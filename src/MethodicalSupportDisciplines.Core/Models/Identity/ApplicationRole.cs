using Microsoft.AspNetCore.Identity;

namespace MethodicalSupportDisciplines.Core.Models.Identity;

public class ApplicationRole : IdentityRole
{
    public string? RoleDescription { get; set; }
    
    public ApplicationRole(string name, string roleDescription)
        : base(name)
    {
        RoleDescription = roleDescription;
    }
}