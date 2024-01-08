using MethodicalSupportDisciplines.Shared.Responses.Services;
using Microsoft.AspNetCore.Http;

namespace MethodicalSupportDisciplines.BLL.Interfaces;

public interface IFileService
{
    Task<FileResponse> UploadMaterialFileAsync(IFormFile formFile, int materialTypeId);
}