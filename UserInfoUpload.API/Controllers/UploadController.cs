using Common;
using Common.DTOs;
using Common.Models;
using Common.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserInfoUpload.API.Dto;
using UserInfoUpload.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserInfoUpload.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IFaceDetectionService _faceDetectionService;

        public UploadController(ApplicationDbContext applicationDbContext, IFaceDetectionService faceDetectionService)
        {
            _applicationDbContext = applicationDbContext;
            _faceDetectionService = faceDetectionService;
        }
        [HttpPost]
        [Route("UploadImages")]
        public async Task<IActionResult> UploadImages([FromForm] UploadImagesDto payload)
        {
            const long maxFileSize = 20 * 1024 * 1024; // 20MB

            if (payload.FrontDrivingLicense == null || payload.BackDrivingLicense == null || payload.Selfie == null)
            {
                return BadRequest("Please upload all three images: Front of Driver License, Back of Driver License, and Selfie.");
            }

            try
            {
                var helper = new ImageHelper();
                var basePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images");
                // Validate and save the Front of Driver License
                var frontImagePath = await helper.SaveImage(payload.FrontDrivingLicense, maxFileSize, "Front of Driver License", basePath);

                // Validate and save the Back of Driver License
                var backImagePath = await helper.SaveImage(payload.BackDrivingLicense, maxFileSize, "Back of Driver License", basePath);

                // Validate and save the Selfie
                var selfieImagePath = await helper.SaveImage(payload.Selfie, maxFileSize, "Selfie", basePath);

                // compare faces
                // Detect faces in the driving license image
                string frontImageFileName = frontImagePath.Split('/').Last();
                var frontDrivingLicenseFaces = _faceDetectionService.DetectAndSaveFaces(Path.Join(basePath, frontImageFileName));
                if (frontDrivingLicenseFaces.Count == 0)
                {
                    string message = "No face detected in the driving license image.";                    
                }

                // Detect faces in the selfie image
                List<(string, string)> profileImageFaces = new List<(string, string)>();
                string selfieImageFileName = selfieImagePath.Split('/').Last();
                profileImageFaces = _faceDetectionService.DetectAndSaveFaces(Path.Join(basePath, selfieImageFileName));
                if (profileImageFaces.Count == 0)
                {
                    string message = "No face detected in selfie image.";
                }

                // Compare faces and calculate similarity
                decimal highestSimilarity = -10;
                foreach (var licenseFace in frontDrivingLicenseFaces)
                {
                    foreach (var selfieFace in profileImageFaces)
                    {
                        var faceMatchResult = await _faceDetectionService.MatchFaces(licenseFace.Item2, selfieFace.Item2);
                        var faceMatchResponse = JsonConvert.DeserializeObject<FaceMatchResponse>(faceMatchResult);

                        if (faceMatchResponse?.Similarity > highestSimilarity)
                        {
                            highestSimilarity = faceMatchResponse.Similarity;
                        }
                    }
                }

                //TODO: Fix it
                // Save the result in the database (optional)
                //var userAttemptHistory = new UserAttemptHistory
                //{
                //    UserId = 0, // Replace with the actual user ID if available
                //    TimeStamp = DateTime.UtcNow,
                //    Result = highestSimilarity >= 80 ? "Success" : "Fail", // Example threshold: 80%
                //    Details = $"Driving License vs Selfie Comparison",
                //    Similarity = $"{highestSimilarity}%"
                //};
                //_context.UserAttemptHistories.Add(userAttemptHistory);
                //await _context.SaveChangesAsync();

                // Display the result to the user
                
                //

                // Insert entry into the database
                //var newEntry = new DrivingLicenseInfo
                //{
                //    FrontDrivingLicenseImagePath = frontImagePath,
                //    BackDrivingLicenseImagePath = backImagePath,
                //    CreatedAt = DateTime.UtcNow
                //};

                //var selfieEntry = new UserImage
                //{
                //    ImagePath = selfieImagePath,
                //    UserId = 0 // Replace with actual user ID if available
                //};

                //_applicationDbContext.DrivingLicenseInfos.Add(newEntry);
                //_applicationDbContext.DrivingLicenseImages.Add(new DrivingLicenseImage
                //{
                //    ImagePath = frontImagePath,
                //    Side = DrivingLicenseImageSide.Front.ToString(),
                //    SideId = (int)DrivingLicenseImageSide.Front,
                //    UserId = 0 // Replace with actual user ID if available
                //});
                //_applicationDbContext.DrivingLicenseImages.Add(new DrivingLicenseImage
                //{
                //    ImagePath = backImagePath,
                //    Side = DrivingLicenseImageSide.Back.ToString(),
                //    SideId = (int)DrivingLicenseImageSide.Back,
                //    UserId = 0 // Replace with actual user ID if available
                //});
                //_applicationDbContext.UserImages.Add(selfieEntry);

                //await _applicationDbContext.SaveChangesAsync();

                //return Ok(new
                //{
                //    message = "Images uploaded successfully.",
                //    frontImagePath,
                //    backImagePath,
                //    selfieImagePath
                //});
                return Ok(new
                {
                    message = highestSimilarity <= 0 ? "Face not matched" : "Face matched",
                    Similarity = highestSimilarity * 100                   
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
