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
        public string LicenseNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string ExpirationDate { get; set; }
        public string IssueDate { get; set; }
        public string IssuingJurisdiction { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string OrganDonor { get; set; }
        public string Address { get; set; }
        public string Class { get; set; }
        public string Restrictions { get; set; }
        public string Endorsements { get; set; }
        public string CustomerId { get; set; }
        public string PlaceOfBirth { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string DocumentDiscriminator { get; set; }
        public string Country { get; set; }
        public string FederalCompliance { get; set; }
        // Add more fields as needed
    }
}
