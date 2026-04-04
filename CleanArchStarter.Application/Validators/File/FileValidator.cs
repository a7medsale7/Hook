using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

namespace Hook.Application.Validators.File;

public class FileValidator : AbstractValidator<IFormFile>
{
    public FileValidator()
    {
        RuleFor(x => x).NotEmpty().WithMessage("Image file is required.");
        RuleFor(x => x.Length).LessThanOrEqualTo(5 * 1024 * 1024).WithMessage("File size must be less than 5MB.");
        RuleFor(x => x.FileName).Must(HaveValidExtension).WithMessage("Invalid file extension. Only .jpg, .jpeg, .png are allowed.");
    }

    private bool HaveValidExtension(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLower();
        string[] validExtensions = { ".jpg", ".jpeg", ".png" };
        return validExtensions.Contains(extension);
    }
}
