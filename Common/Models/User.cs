using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property for multiple images
        public List<UserImage> UserImages { get; set; } = new List<UserImage>();
        public List<DrivingLicenseImage> DrivingLicenseImages { get; set; } = new List<DrivingLicenseImage>();        
        public List<RealIdImage> RealIdImages { get; set; } = new List<RealIdImage>();
        public List<UserAttemptHistory> UserAttemptsHistories { get; set; } = new List<UserAttemptHistory>();
    }
}
