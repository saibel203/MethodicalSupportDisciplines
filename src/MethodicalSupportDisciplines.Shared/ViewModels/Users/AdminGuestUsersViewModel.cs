using MethodicalSupportDisciplines.Shared.Dto.Users;
using MethodicalSupportDisciplines.Shared.ViewModels.Additional;

namespace MethodicalSupportDisciplines.Shared.ViewModels.Users;

public class AdminGuestUsersViewModel : SettingsViewModel
{
    public IEnumerable<GetGuestUserDto> GuestUsers { get; set; } = new List<GetGuestUserDto>();
}