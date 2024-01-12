namespace MethodicalSupportDisciplines.Shared.Responses.Services;

public class FileResponse : BaseResponse
{
    public string FilePath { get; set; } = string.Empty;
    public int CreateMaterialId { get; set; }
}