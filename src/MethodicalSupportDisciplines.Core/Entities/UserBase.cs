using MethodicalSupportDisciplines.Core.Models.Identity;

namespace MethodicalSupportDisciplines.Core.Entities;

public class UserBase
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    
    public string ApplicationUserId { get; set; } = string.Empty;
    public ApplicationUser? ApplicationUser { get; set; }
}