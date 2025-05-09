namespace UserInfoUpload.API.Services
{
    public class ImageHelper
    {
        public async Task<string> SaveImage(IFormFile image, long maxFileSize, string imageType, string savePath = "")
        {
            if (!image.ContentType.StartsWith("image/"))
            {
                throw new ArgumentException($"{imageType} is not a valid image. Only image files are allowed.");
            }

            if (image.Length > maxFileSize)
            {
                throw new ArgumentException($"{imageType} exceeds the maximum allowed size of {maxFileSize / (1024 * 1024)}MB.");
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            if (string.IsNullOrEmpty(savePath))
            {
                savePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images");
            }
            if (Directory.Exists(savePath) == false)
            {
                Directory.CreateDirectory(savePath);
            }

            var uploadPath = Path.Combine(savePath, uniqueFileName);           
            
            
            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return $"/images/{uniqueFileName}";
        }

    }
}
