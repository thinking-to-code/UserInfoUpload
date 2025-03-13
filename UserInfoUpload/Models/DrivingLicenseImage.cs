using System.ComponentModel.DataAnnotations;

namespace UserInfoUpload.Models
{
    public class DrivingLicenseImage
    {
        public int Id { get; set; }

        [Required]
        public string ImagePath { get; set; }

        // Foreign key to associate images with users
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
