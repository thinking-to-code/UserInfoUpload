using System.ComponentModel.DataAnnotations;

namespace UserInfoUpload.Models
{
    public class DrivingLicenseInfo
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [StringLength(100)]
        public string DrivingLicenseNumber { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(25)]
        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;

        public DateTime IssueDate { get; set; } = DateTime.UtcNow;        
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string FrontDrivingLicenseImagePath { get; set; }
        public string BackDrivingLicenseImagePath { get; set; }

        public User User { get; set; }
    }
}
