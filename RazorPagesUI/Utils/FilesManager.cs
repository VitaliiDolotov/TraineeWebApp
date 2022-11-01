using Microsoft.AspNetCore.Hosting;
using RazorPagesDemo.Models;

namespace RazorPagesUI.Utils
{
    public static class FilesManager
    {
        private const string ImagesFolder = "images";
        private const string ProfileImagesFolder = "profileImages";

        public static string ProcessUploadProfileImage(IWebHostEnvironment webHostEnvironment, IFormFile? image)
        {
            string uniqueFileName = string.Empty;

            if (image is not null)
            {
                var uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, ImagesFolder, ProfileImagesFolder);
                uniqueFileName = $"{Guid.NewGuid()}_{image.FileName}";
                var filePath = Path.Combine(uploadFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                image.CopyTo(fileStream);
            }

            return uniqueFileName;
        }

        public static void DeleteExistingProfileImage(IWebHostEnvironment webHostEnvironment, User user)
        {
            if (user.ProfileImage is not null)
            {
                var filePath = Path.Combine(webHostEnvironment.WebRootPath, ImagesFolder, ProfileImagesFolder, user.ProfileImage);
                File.Delete(filePath);
            }
        }

        public static void ClearProfileImagesFolder(IWebHostEnvironment webHostEnvironment)
        {
            var profileImagesFolder = Path.Combine(webHostEnvironment.WebRootPath, ImagesFolder, ProfileImagesFolder);
            var files = Directory.GetFiles(profileImagesFolder);
            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch { }
            }
        }
    }
}
