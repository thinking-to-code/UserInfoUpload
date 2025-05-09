namespace UserInfoUpload.API.Dto
{
    public class UploadImagesDto
    {
        public IFormFile FrontDrivingLicense { get; set; }
        public IFormFile BackDrivingLicense { get; set; }
        public IFormFile Selfie { get; set; }
    }
}
