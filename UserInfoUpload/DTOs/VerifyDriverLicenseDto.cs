﻿using System.ComponentModel.DataAnnotations;

namespace UserInfoUpload.DTOs
{
    public class VerifyDriverLicenseDto
    {
        public string DrivingLicenseNumber { get; set; }
        public string FrontDrivingLicenseImagePath { get; set; }
        public string BackDrivingLicenseImagePath { get; set; }
    }
}
