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

        //Old implementation
        // POST: User/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Upload(List<IFormFile> DrivingLicenseImages, List<IFormFile> ProfileImages)
        //{
        //    const long maxFileSize = 20 * 1024 * 1024; // 20MB

        //    if (DrivingLicenseImages != null)
        //    {
        //        foreach (var file in DrivingLicenseImages)
        //        {
        //            if (!file.ContentType.StartsWith("image/"))
        //            {
        //                ModelState.AddModelError("DrivingLicenseImages", "Only image files are allowed.");
        //            }
        //            if (file.Length > maxFileSize)
        //            {
        //                ModelState.AddModelError("DrivingLicenseImages", "Each image file must be 20MB or less.");
        //            }
        //        }
        //    }

        //    if (ProfileImages != null)
        //    {
        //        foreach (var file in ProfileImages)
        //        {
        //            if (!file.ContentType.StartsWith("image/"))
        //            {
        //                ModelState.AddModelError("ProfileImages", "Only image files are allowed.");
        //            }
        //            if (file.Length > maxFileSize)
        //            {
        //                ModelState.AddModelError("ProfileImages", "Each image file must be 20MB or less.");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("ProfileImages", "Please provide Selfie image. We don't have selfie image");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        //_context.Add(user);
        //        //await _context.SaveChangesAsync(); // Save user first to get UserId
        //        if (DrivingLicenseImages != null && DrivingLicenseImages.Count > 0)
        //        {
        //            foreach (var drivingLicenseImage in DrivingLicenseImages)
        //            {
        //                if (drivingLicenseImage.Length > 0)
        //                {
        //                    // Save driving license image on server
        //                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(drivingLicenseImage.FileName);
        //                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

        //                    using (var stream = new FileStream(uploadPath, FileMode.Create))
        //                    {
        //                        await drivingLicenseImage.CopyToAsync(stream);
        //                    }

        //                    // Read the file and detect faces
        //                    var faceImagePaths = _faceDetectionService.DetectAndSaveFaces(uploadPath);

        //                    // Loop all the provided selfie images and compare with the detected faces
        //                    if (ProfileImages != null && ProfileImages.Count > 0 && faceImagePaths.Count > 0)
        //                    {
        //                        foreach (var profileImage in ProfileImages)
        //                        {
        //                            if (profileImage.Length > 0)
        //                            {
        //                                uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(profileImage.FileName);
        //                                uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

        //                                using (var stream = new FileStream(uploadPath, FileMode.Create))
        //                                {
        //                                    await profileImage.CopyToAsync(stream);
        //                                }

        //                                var selfieCroppedFaceImagePaths = _faceDetectionService.DetectAndSaveFaces(uploadPath);

        //                                decimal similarity = 0;
        //                                foreach (var faceImagePath in faceImagePaths)
        //                                {
        //                                    var faceMatchResult = await _faceDetectionService.MatchFaces(faceImagePath.Item2, selfieCroppedFaceImagePaths[0].Item2);
        //                                    var faceMatchResponse = JsonConvert.DeserializeObject<FaceMatchResponse>(faceMatchResult);
        //                                    if (faceMatchResponse?.Similarity > similarity)
        //                                    {
        //                                        similarity = faceMatchResponse.Similarity;
        //                                    }
        //                                }
        //                                TempData["Similarity"] = $"Similarity: {similarity}";
        //                            }
        //                        }                                
        //                    }
        //                    else
        //                    {
        //                        TempData["ErrorMessage"] = $"No face detected on provided images. Please try again.";
        //                    }
        //                }
        //            }
        //        }              

        //        // Redirect to the Create page to reset the form
        //        //return RedirectToAction("Create");

        //        return RedirectToAction(nameof(Upload));
        //    }

        //    return View(nameof(Upload));
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(List<IFormFile> DrivingLicenseImages, List<IFormFile> ProfileImages,  string capturedImage = "")
        {
            const long maxFileSize = 20 * 1024 * 1024; // 20MB

            if (DrivingLicenseImages == null || DrivingLicenseImages.Count == 0)
            {
                string errorMessage = "Please upload at least one driving license image.";
                ModelState.AddModelError("DrivingLicenseImages", errorMessage);
                TempData["ErrorMessage"] = errorMessage;
            }            
            if ((ProfileImages == null || ProfileImages.Count == 0) && string.IsNullOrEmpty(capturedImage))
            {
                string errorMessage = "Please provide a selfie image (either uploaded or captured).";
                ModelState.AddModelError("capturedImage", "Please provide a selfie image (either uploaded or captured).");
                TempData["ErrorMessage"] = errorMessage;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Save the driving license image
                    string drivingLicenseImagePath = null;
                    foreach (var drivingLicenseImage in DrivingLicenseImages)
                    {
                        if (drivingLicenseImage.Length > 0)
                        {
                            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(drivingLicenseImage.FileName);
                            drivingLicenseImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                            using (var stream = new FileStream(drivingLicenseImagePath, FileMode.Create))
                            {
                                await drivingLicenseImage.CopyToAsync(stream);
                            }
                        }
                    }

                    // Save the captured selfie image
                    string selfieImagePath = null;
                    string profileImagePath = null;
                    if (!string.IsNullOrEmpty(capturedImage))
                    {
                        string uniqueFileName = Guid.NewGuid().ToString() + ".png";
                        selfieImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                        // Convert Base64 string to image and save
                        var imageBytes = Convert.FromBase64String(capturedImage.Split(',')[1]);
                        await System.IO.File.WriteAllBytesAsync(selfieImagePath, imageBytes);
                    }
                    else
                    {
                        // Save the selfie image
                        
                        foreach (var profileImage in ProfileImages)
                        {
                            if (profileImage.Length > 0)
                            {
                                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(profileImage.FileName);
                                profileImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                                using (var stream = new FileStream(profileImagePath, FileMode.Create))
                                {
                                    await profileImage.CopyToAsync(stream);
                                }
                            }
                        }
                    }

                    // Detect faces in the driving license image
                    var drivingLicenseFaces = _faceDetectionService.DetectAndSaveFaces(drivingLicenseImagePath);
                    if (drivingLicenseFaces.Count == 0)
                    {
                        TempData["ErrorMessage"] = "No face detected in the driving license image.";
                        return RedirectToAction(nameof(Upload));
                    }

                    // Detect faces in the selfie image
                    List<(string, string)> profileImageFaces = new List<(string, string)>();
                    if (string.IsNullOrEmpty(selfieImagePath))
                    {
                        profileImageFaces = _faceDetectionService.DetectAndSaveFaces(profileImagePath);
                        if (profileImageFaces.Count == 0)
                        {
                            TempData["ErrorMessage"] = "No face detected in the Selfie image.";
                            return RedirectToAction(nameof(Upload));
                        }
                    }
                    else
                    {
                        profileImageFaces = _faceDetectionService.DetectAndSaveFaces(selfieImagePath);
                        if (profileImageFaces.Count == 0)
                        {
                            TempData["ErrorMessage"] = "No face detected in the selfie image.";
                            return RedirectToAction(nameof(Upload));
                        }
                    }

                    // Compare faces and calculate similarity
                    decimal highestSimilarity = -10;
                    foreach (var licenseFace in drivingLicenseFaces)
                    {
                        foreach (var selfieFace in profileImageFaces)
                        {
                            var faceMatchResult = await _faceDetectionService.MatchFaces(licenseFace.Item2, selfieFace.Item2);
                            var faceMatchResponse = JsonConvert.DeserializeObject<FaceMatchResponse>(faceMatchResult);

                            if (faceMatchResponse?.Similarity > highestSimilarity)
                            {
                                highestSimilarity = faceMatchResponse.Similarity * 100;
                            }
                        }
                    }

                    //TODO: Fix it
                    //Save the result in the database(optional)

                    IronBarcodeReaderService barcodeReaderService = new IronBarcodeReaderService(_configuration);
                    var barcodeDecodedText = barcodeReaderService.DecodeBarcode(new System.Drawing.Bitmap(drivingLicenseImagePath), drivingLicenseImagePath);
                    var barcodeDetails = barcodeReaderService.ParseAamvaToModel(barcodeDecodedText);

                    //Read matching user from DB
                    if (!string.IsNullOrEmpty(barcodeDetails.LicenseNumber))
                    {
                        var fromDBUser = _context.DrivingLicenseImages.FirstOrDefault(p => p.LicenseNumber == barcodeDetails.LicenseNumber);
                        if (fromDBUser != null)
                        {
                            var userAttemptHistory = new UserAttemptHistory
                            {
                                UserId = fromDBUser.UserId, // Replace with the actual user ID if available
                                TimeStamp = DateTime.UtcNow,
                                Result = highestSimilarity >= 80 ? "Success" : "Fail", // Example threshold: 80%
                                Details = $"Driving License vs Selfie Comparison",
                                Similarity = $"{highestSimilarity}%"
                            };
                            _context.UserAttemptHistories.Add(userAttemptHistory);
                            await _context.SaveChangesAsync();
                        }
                    }
                    

                    // Display the result to the user
                    TempData["Similarity"] = $"Similarity: {highestSimilarity}%";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                }
            }

            return View(nameof(Upload));
        }


    }
}
