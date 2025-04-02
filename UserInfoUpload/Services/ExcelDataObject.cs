﻿namespace UserInfoUpload.Services
{
    public enum Vendor
    {
        ZXing = 1,
        IronBarcode,
        Dynamsoft
    }
    public class ExcelDataObject
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string SavedFacePath { get; set; }
        public int DetectedFaceCount { get; set; }
        public string Vendor { get; set; }
    }
}
