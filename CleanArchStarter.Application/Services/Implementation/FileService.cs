using Hook.Application.Abstractions;
using Hook.Application.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Hook.Application.Services.Implementation;

public class FileService(IWebHostEnvironment webHostEnvironment) : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

    public async Task<string> SaveFileAsync(IFormFile file, string folderName)
    {
        // استخدام WebRootPath من مشروع الـ API (wwwroot)
        var path = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(path, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/{folderName}/{fileName}";
    }

    public void DeleteFile(string? filePath)
    {
        if (string.IsNullOrEmpty(filePath)) return;

        var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath.TrimStart('/'));
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }
}
