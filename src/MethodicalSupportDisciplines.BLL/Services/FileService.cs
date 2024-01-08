using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Data.Interfaces.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;
using MethodicalSupportDisciplines.Shared.Responses.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.BLL.Services;

public class FileService : IFileService
{
    private readonly ILogger<FileService> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IMaterialRepository _materialRepository;
    private readonly IStringLocalizer<FileService> _stringLocalization;

    public FileService(ILogger<FileService> logger, IWebHostEnvironment webHostEnvironment,
        IMaterialRepository materialRepository, IStringLocalizer<FileService> stringLocalization)
    {
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
        _materialRepository = materialRepository;
        _stringLocalization = stringLocalization;
    }

    public async Task<FileResponse> UploadMaterialFileAsync(IFormFile formFile, int materialTypeId)
    {
        try
        {
            const string fileDirectory = "files";

            string webRootPath = _webHostEnvironment.WebRootPath;
            string uploadDirectory = Path.Combine(webRootPath, fileDirectory);
            string fileExtension = Path.GetExtension(formFile.FileName);
            string fileName = ImageNameGeneration();
            string resultPath = Path.Combine(fileDirectory, fileName + fileExtension);

            if (formFile.Length < 0)
            {
                return new FileResponse
                {
                    Message = _stringLocalization["FormFileGetError"],
                    IsSuccess = false
                };
            }

            IsDirectoryExists(uploadDirectory);

            await using FileStream fileStream =
                new FileStream(Path.Combine(uploadDirectory, fileName + fileExtension), FileMode.Create);
            await formFile.CopyToAsync(fileStream);
            await fileStream.FlushAsync();

            Material uploadMaterial = new Material
            {
                MaterialPath = resultPath,
                MaterialTypeId = materialTypeId
            };

            MaterialRepositoryResponse createResponse =
                await _materialRepository.CreateMaterialAsync(uploadMaterial);

            if (!createResponse.IsSuccess)
            {
                return new FileResponse
                {
                    Message = createResponse.Message,
                    IsSuccess = false
                };
            }

            return new FileResponse
            {
                Message = _stringLocalization["Success"],
                IsSuccess = true,
                CreateMaterialId = createResponse.CreatedMaterialId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to upload a file.");

            return new FileResponse
            {
                Message = _stringLocalization["UnknownError"],
                IsSuccess = false
            };
        }
    }

    private void IsDirectoryExists(string uploadDirectory)
    {
        if (!Directory.Exists(uploadDirectory))
        {
            Directory.CreateDirectory(uploadDirectory);
        }
    }

    private string ImageNameGeneration()
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, 50)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());
    }
}