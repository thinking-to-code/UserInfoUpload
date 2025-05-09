using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class UserAttemptHistory
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }        
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        [StringLength(100)]
        public string Result { get; set; } //Success or Fail

        [StringLength(500)]
        public string Details { get; set; }

        [StringLength(100)]
        public string Similarity { get; set; }

        // Navigation property for multiple images       

        public User User { get; set; }
    }
}
