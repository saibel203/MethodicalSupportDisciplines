using MethodicalSupportDisciplines.Shared.Dto.Users;
using MethodicalSupportDisciplines.Shared.ViewModels.Additional;

namespace MethodicalSupportDisciplines.Shared.ViewModels.Users;

public class AdminTeacherUsersViewModel : SettingsViewModel
{
    public IEnumerable<GetTeacherUserDto> TeacherUsers { get; set; } = new List<GetTeacherUserDto>();
}