using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CoffeeWebApp.Helpers;
using CoffeeWebApp.Interfaces;
using Microsoft.Extensions.Options;
using System.Security.Policy;

namespace CoffeeWebApp.Service
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloundinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                    );
            _cloudinary = new Cloudinary(acc);
        }
        public async Task<DeletionResult> DeletePhotoAsync(string url)
        {
            Uri uri = new Uri(url);
            string publicId = uri.Segments[4];
            var deleteParms = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParms);

            return result;

        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file != null)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(300).Width(300).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }
        


    }
}
