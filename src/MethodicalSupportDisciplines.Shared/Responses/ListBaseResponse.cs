namespace MethodicalSupportDisciplines.Shared.Responses;

public class ListBaseResponse : BaseResponse
{
    public int PageCount { get; set; }
    public int ItemsCount { get; set; }
    public string? SearchString { get; set; } = string.Empty;
    public IEnumerable<int> Pages { get; set; } = new List<int>();
}