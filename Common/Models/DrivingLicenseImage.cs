using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class DrivingLicenseImage
    {
        public int Id { get; set; }

        [Required]
        public string ImagePath { get; set; }

        // Foreign key to associate images with users
        public int UserId { get; set; }

        public int SideId { get; set; } //1. Front or 2. Back
        public string Side { get; set; } //1. Front or 2. Back
        public User User { get; set; }
    }

    public enum DrivingLicenseImageSide
    {
        Front = 1,
        Back = 2
    }
}
