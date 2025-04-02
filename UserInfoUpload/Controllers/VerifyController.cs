using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserInfoUpload.Data;
using UserInfoUpload.DTOs;
using UserInfoUpload.Models;
using UserInfoUpload.Services;

namespace UserInfoUpload.Controllers
{
    public class VerifyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly FaceDetectionService _faceDetectionService;
        public VerifyController(ApplicationDbContext context, IConfiguration configuration, FaceDetectionService faceDetectionService)
        {
            _context = context;
            _configuration = configuration;
            _faceDetectionService = faceDetectionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(List<IFormFile> DrivingLicenseImages, List<IFormFile> ProfileImages)
        {
            const long maxFileSize = 20 * 1024 * 1024; // 20MB

            if (DrivingLicenseImages != null)
            {
                foreach (var file in DrivingLicenseImages)
                {
                    if (!file.ContentType.StartsWith("image/"))
                    {
                        ModelState.AddModelError("DrivingLicenseImages", "Only image files are allowed.");
                    }
                    if (file.Length > maxFileSize)
                    {
                        ModelState.AddModelError("DrivingLicenseImages", "Each image file must be 20MB or less.");
                    }
                }
            }

            if (ProfileImages != null)
            {
                foreach (var file in ProfileImages)
                {
                    if (!file.ContentType.StartsWith("image/"))
                    {
                        ModelState.AddModelError("ProfileImages", "Only image files are allowed.");
                    }
                    if (file.Length > maxFileSize)
                    {
                        ModelState.AddModelError("ProfileImages", "Each image file must be 20MB or less.");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("ProfileImages", "Please provide Selfie image. We don't have selfie image");
            }

            if (ModelState.IsValid)
            {
                //_context.Add(user);
                //await _context.SaveChangesAsync(); // Save user first to get UserId
                if (DrivingLicenseImages != null && DrivingLicenseImages.Count > 0)
                {
                    foreach (var drivingLicenseImage in DrivingLicenseImages)
                    {
                        if (drivingLicenseImage.Length > 0)
                        {
                            // Save driving license image on server
                            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(drivingLicenseImage.FileName);
                            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                            using (var stream = new FileStream(uploadPath, FileMode.Create))
                            {
                                await drivingLicenseImage.CopyToAsync(stream);
                            }

                            // Read the file and detect faces
                            var faceImagePaths = _faceDetectionService.DetectAndSaveFaces(uploadPath);

                            // Loop all the provided selfie images and compare with the detected faces
                            if (ProfileImages != null && ProfileImages.Count > 0 && faceImagePaths.Count > 0)
                            {
                                foreach (var profileImage in ProfileImages)
                                {
                                    if (profileImage.Length > 0)
                                    {
                                        uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(profileImage.FileName);
                                        uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                                        using (var stream = new FileStream(uploadPath, FileMode.Create))
                                        {
                                            await profileImage.CopyToAsync(stream);
                                        }

                                        var selfieCroppedFaceImagePaths = _faceDetectionService.DetectAndSaveFaces(uploadPath);

                                        foreach (var faceImagePath in faceImagePaths)
                                        {
                                            var faceMatchResult = await _faceDetectionService.MatchFaces(faceImagePath.Item2, selfieCroppedFaceImagePaths[0].Item2);
                                            var faceMatchResponse = JsonConvert.DeserializeObject<FaceMatchResponse>(faceMatchResult);
                                            TempData["Similarity"] = $"Similarity: {faceMatchResponse.Similarity}";

                                        }
                                    }
                                }                                
                            }
                            else
                            {
                                TempData["ErrorMessage"] = $"No face detected on provided images. Please try again.";
                            }
                        }
                    }
                }              

                // Redirect to the Create page to reset the form
                //return RedirectToAction("Create");

                return RedirectToAction(nameof(Upload));
            }

            return View(nameof(Upload));
        }
    }
}
