﻿using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class UserImage
    {
        public int Id { get; set; }

        [Required]
        public string ImagePath { get; set; }

        // Foreign key to associate images with users
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
