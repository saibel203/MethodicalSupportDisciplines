using MethodicalSupportDisciplines.Shared.Dto.Users;
using MethodicalSupportDisciplines.Shared.ViewModels.Additional;

namespace MethodicalSupportDisciplines.Shared.ViewModels.Users;

public class AdminStudentUsersViewModel : SettingsViewModel
{
    public IEnumerable<GetStudentUserDto> StudentUsers { get; set; } = new List<GetStudentUserDto>();

}