using MethodicalSupportDisciplines.Shared.AdditionalModels;

namespace MethodicalSupportDisciplines.Shared.ViewModels.Additional;

public class SettingsViewModel
{
    public int PageCount { get; set; }
    public int ItemsCount { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? CurrentController { get; set; } = string.Empty;
    public string? CurrentAction { get; set; } = string.Empty;
    
    public QueryParameters QueryParameters { get; set; } = new();
    public IEnumerable<int> Pages { get; set; } = new List<int>();
}