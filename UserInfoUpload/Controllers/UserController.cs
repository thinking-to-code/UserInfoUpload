using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserInfoUpload.Data;
using UserInfoUpload.Models;

namespace UserInfoUpload.Controllers
{    
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public UserController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var currentUser = User.Identity.Name;

            if (HttpContext.Session.GetString("IsAuthenticated") == "true")
            {
                var users = await _context.Users.Include(u => u.UserImages).Include(u => u.DrivingLicenseImages).ToListAsync(); // Replace with your actual data retrieval logic
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
        public async Task<IActionResult> Create(User user, List<IFormFile> ProfileImages, List<IFormFile> DrivingLicenseImages)
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
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null) return NotFound();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }    
}
