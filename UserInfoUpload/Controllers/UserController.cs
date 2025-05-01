using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserInfoUpload.Data;
using UserInfoUpload.Models;
using UserInfoUpload.Services;

namespace UserInfoUpload.Controllers
{    
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly FaceDetectionService _faceDetectionService;
        public UserController(ApplicationDbContext context, IConfiguration configuration, FaceDetectionService faceDetectionService)
        {
            _context = context;
            _configuration = configuration;
            _faceDetectionService = faceDetectionService;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var currentUser = User.Identity.Name;

            if (HttpContext.Session.GetString("IsAuthenticated") == "true")
            {
                var users = await _context.Users
                    .Include(u => u.UserImages)
                    .Include(u => u.RealIdImages)
                    .Include(u => u.DrivingLicenseImages)                    
                    .ToListAsync(); // Replace with your actual data retrieval logic
                return View(users);
            }

            return RedirectToAction("EnterPassword");
        }

        // Show password input form
        public IActionResult EnterPassword()
        {
            return View();
        }

        // Validate Password
        [HttpPost]
        public IActionResult EnterPassword(string password)
        {
            var correctPassword = _configuration["AdminPassword"]; // Get password from appsettings.json

            if (password == correctPassword)
            {
                HttpContext.Session.SetString("IsAuthenticated", "true");
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Incorrect password. Try again.";
            return View();
        }

        // Logout (optional)
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAuthenticated");
            return RedirectToAction("EnterPassword");
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user, List<IFormFile> ProfileImages, List<IFormFile> RealIdImages, List<IFormFile> DrivingLicenseImages)
        {
            const long maxFileSize = 10 * 1024 * 1024; // 10MB

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
                        ModelState.AddModelError("ProfileImages", "Each image file must be 10MB or less.");
                    }
                }
            }

            if (RealIdImages != null)
            {
                foreach (var file in RealIdImages)
                {
                    if (!file.ContentType.StartsWith("image/"))
                    {
                        ModelState.AddModelError("RealIdImages", "Only image files are allowed.");
                    }
                    if (file.Length > maxFileSize)
                    {
                        ModelState.AddModelError("RealIdImages", "Each image file must be 10MB or less.");
                    }
                }
            }

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
                        ModelState.AddModelError("DrivingLicenseImages", "Each image file must be 10MB or less.");
                    }
                }
            }

            if (ModelState.IsValid)
            {                
                _context.Add(user);
                await _context.SaveChangesAsync(); // Save user first to get UserId

                if (ProfileImages != null && ProfileImages.Count > 0)
                {
                    foreach (var image in ProfileImages)
                    {
                        if (image.Length > 0)
                        {
                            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                            using (var stream = new FileStream(uploadPath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }

                            // Save image details in database
                            _context.UserImages.Add(new UserImage
                            {
                                UserId = user.Id,                                
                                ImagePath = "/images/" + uniqueFileName
                            });
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                if (RealIdImages != null && RealIdImages.Count > 0)
                {
                    foreach (var image in RealIdImages)
                    {
                        if (image.Length > 0)
                        {
                            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                            using (var stream = new FileStream(uploadPath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }

                            // Save image details in database
                            _context.RealIdImages.Add(new RealIdImage
                            {
                                UserId = user.Id,
                                ImagePath = "/images/" + uniqueFileName
                            });
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                if (DrivingLicenseImages != null && DrivingLicenseImages.Count > 0)
                {
                    foreach (var image in DrivingLicenseImages)
                    {
                        if (image.Length > 0)
                        {
                            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                            using (var stream = new FileStream(uploadPath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }

                            // Save image details in database
                            _context.DrivingLicenseImages.Add(new DrivingLicenseImage
                            {
                                UserId = user.Id,                                
                                ImagePath = "/images/" + uniqueFileName
                            });
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                TempData["SuccessMessage"] = "Data saved successfully!";

                // Redirect to the Create page to reset the form
                //return RedirectToAction("Create");

                return RedirectToAction(nameof(Create));
            }
            return View(user);
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users
                .Include(u => u.UserImages)
                .Include(dl => dl.DrivingLicenseImages)
                .Include(ri => ri.RealIdImages)
                .Include(u => u.UserAttemptsHistories)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null) return NotFound();

            return View(user);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users
                .Include(u => u.UserImages)
                .Include(u => u.DrivingLicenseImages)
                .Include(u => u.RealIdImages)
                .Include(u => u.UserAttemptsHistories)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null) return NotFound();

            // Remove associated images
            _context.UserImages.RemoveRange(user.UserImages);
            _context.DrivingLicenseImages.RemoveRange(user.DrivingLicenseImages);
            _context.RealIdImages.RemoveRange(user.RealIdImages);
            _context.UserAttemptHistories.RemoveRange(user.UserAttemptsHistories);

            // Remove the user
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: User/FaceDetection
        public IActionResult FaceDetection()
        {
            return View();
        }

        // POST: User/FaceDetection
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FaceDetection(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                const long maxFileSize = 20 * 1024 * 1024; // 20MB

                if (!imageFile.ContentType.StartsWith("image/"))
                {
                    ModelState.AddModelError("ProfileImages", "Only image files are allowed.");
                }
                if (imageFile.Length > maxFileSize)
                {
                    ModelState.AddModelError("ProfileImages", "Each image file must be 20MB or less.");
                }

                if (ModelState.IsValid)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    var faceImagePaths = _faceDetectionService.DetectAndSaveFaces(uploadPath);
                    ViewBag.FaceImagePaths = faceImagePaths;
                    ViewBag.SelectedImagePath = $"/images/{uniqueFileName}";
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(int userId, IFormFile ProfileImage)
        {
            if (ProfileImage != null && ProfileImage.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfileImage.FileName);
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(stream);
                }

                // Save the image path to the database
                var userImage = new UserImage
                {
                    UserId = userId,
                    ImagePath = $"/images/{uniqueFileName}"
                };
                _context.UserImages.Add(userImage);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = userId });
        }

        [HttpPost]
        public async Task<IActionResult> UploadRealIdImage(int userId, IFormFile RealIdImage)
        {
            if (RealIdImage != null && RealIdImage.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(RealIdImage.FileName);
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await RealIdImage.CopyToAsync(stream);
                }

                // Save the image path to the database
                var realIdImage = new RealIdImage
                {
                    UserId = userId,
                    ImagePath = $"/images/{uniqueFileName}"
                };
                _context.RealIdImages.Add(realIdImage);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = userId });
        }


        [HttpPost]
        public async Task<IActionResult> UploadDrivingLicenseImage(int userId, IFormFile DrivingLicenseImage)
        {
            if (DrivingLicenseImage != null && DrivingLicenseImage.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(DrivingLicenseImage.FileName);
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await DrivingLicenseImage.CopyToAsync(stream);
                }

                // Save the image path to the database
                var drivingLicenseImage = new DrivingLicenseImage
                {
                    UserId = userId,
                    ImagePath = $"/images/{uniqueFileName}"
                };
                _context.DrivingLicenseImages.Add(drivingLicenseImage);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = userId });
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users
                .Include(u => u.UserImages)
                .Include(u => u.DrivingLicenseImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null) return NotFound();

            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Users.Any(e => e.Id == user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

    }
}
