using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile file, string folderName);
    Task<List<string>> SaveFilesAsync(IFormFileCollection? files, string folderName);
    void DeleteFile(string? filePath);
}
