using IronBarCode;
using System.Drawing;
using System.Text;
using UserInfoUpload.Models;

namespace UserInfoUpload.Services
{
    public class IronBarcodeReaderService
    {
        public IronBarcodeReaderService(IConfiguration configuration)
        {
            IronBarCode.License.LicenseKey = configuration["IronBarcode:LicenseKey"];
        }
        public string DecodeBarcode(Bitmap _selectedImage, string filePath)
        {
            // To configure and fine-tune barcode reading, utilize the BarcodeReaderOptions class
            var myOptionsExample = new BarcodeReaderOptions
            {
                // Choose a reading speed from: Faster, Balanced, Detailed, ExtremeDetail
                // There is a tradeoff in performance as more detail is set
                Speed = ReadingSpeed.Balanced,

                // Reader will stop scanning once a single barcode is found (if set to true)
                ExpectMultipleBarcodes = true,

                // By default, all barcode formats are scanned for
                // Specifying a subset of barcode types to search for would improve performance
                ExpectBarcodeTypes = BarcodeEncoding.PDF417,

                // Utilize multiple threads to read barcodes from multiple images in parallel
                Multithreaded = true,

                // Maximum threads for parallelized barcode reading
                // Default is 4
                MaxParallelThreads = 2,

                // The area of each image frame in which to scan for barcodes
                // Specifying a crop area will significantly improve performance and avoid noisy parts of the image
                //CropArea = new Rectangle(),

                // Special setting for Code39 barcodes
                // If a Code39 barcode is detected, try to read with both the base and extended ASCII character sets
                UseCode39ExtendedMode = true
            };
            BarcodeResults results = BarcodeReader.Read(_selectedImage, myOptionsExample);
            if (results != null && results.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                int i = 1;
                foreach (BarcodeResult result in results)
                {
                    sb.Append(result.Text);
                    //sb.AppendLine($"Barcode Result {i++}: {result.Text}");
                }

                return sb.ToString();
            }
            else
            {
                return "";
            }
        }

        public DrivingLicenseInfo ParseAamvaToModel(string raw)
        {
            var model = new DrivingLicenseInfo();
            var lines = raw.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                if (line.Length < 3) continue;
                var code = line.Substring(0, 3);
                var value = line.Substring(3).Trim();

                switch (code)
                {
                    case "DAQ": model.LicenseNumber = value; break;
                    case "DCS": model.LastName = value; break;
                    case "DAC": model.FirstName = value; break;
                    case "DAD": model.MiddleName = value; break;
                    case "DBB": model.DateOfBirth = value; break;
                    case "DBC": model.Sex = value; break;
                    case "DBA": model.ExpirationDate = value; break;
                    case "DBD": model.IssueDate = value; break;
                    case "DBJ": model.IssuingJurisdiction = value; break;
                    case "DBK": model.SocialSecurityNumber = value; break;
                    case "DBH": model.OrganDonor = value; break;
                    case "DBI": model.Address = value; break;
                    case "DBL": model.Class = value; break;
                    case "DBM": model.Restrictions = value; break;
                    case "DBN": model.Endorsements = value; break;
                    case "DBP": model.CustomerId = value; break;
                    case "DBQ": model.PlaceOfBirth = value; break;
                    case "DAG": model.StreetAddress = value; break;
                    case "DAI": model.City = value; break;
                    case "DAJ": model.State = value; break;
                    case "DAK": model.PostalCode = value; break;
                    case "DCF": model.DocumentDiscriminator = value; break;
                    case "DCG": model.Country = value; break;
                    case "DCH": model.FederalCompliance = value; break;
                        // Add more as needed
                }
            }

            return model;
        }


        public void ReadBarcode(string imagePath)
        {
            // Reading a barcode is easy with IronBarcode!
            var resultFromFile = BarcodeReader.Read(@"file/barcode.png"); // From a file
            var resultFromBitMap = BarcodeReader.Read(new Bitmap("barcode.bmp")); // From a bitmap
            var resultFromImage = BarcodeReader.Read(Image.FromFile("barcode.jpg")); // From an image
            var resultFromPdf = BarcodeReader.ReadPdf(@"file/mydocument.pdf"); // From PDF use ReadPdf

            // To configure and fine-tune barcode reading, utilize the BarcodeReaderOptions class
            var myOptionsExample = new BarcodeReaderOptions
            {
                // Choose a reading speed from: Faster, Balanced, Detailed, ExtremeDetail
                // There is a tradeoff in performance as more detail is set
                Speed = ReadingSpeed.Balanced,

                // Reader will stop scanning once a single barcode is found (if set to true)
                ExpectMultipleBarcodes = true,

                // By default, all barcode formats are scanned for
                // Specifying a subset of barcode types to search for would improve performance
                ExpectBarcodeTypes = BarcodeEncoding.PDF417,

                // Utilize multiple threads to read barcodes from multiple images in parallel
                Multithreaded = true,

                // Maximum threads for parallelized barcode reading
                // Default is 4
                MaxParallelThreads = 2,

                // The area of each image frame in which to scan for barcodes
                // Specifying a crop area will significantly improve performance and avoid noisy parts of the image
                CropArea = new Rectangle(),

                // Special setting for Code39 barcodes
                // If a Code39 barcode is detected, try to read with both the base and extended ASCII character sets
                UseCode39ExtendedMode = true
            };

            // Read with the options applied
            var results = BarcodeReader.Read("barcode.png", myOptionsExample);

            // Create a barcode with one line of code
            var myBarcode = BarcodeWriter.CreateBarcode("12345", BarcodeWriterEncoding.EAN8);

            // After creating a barcode, we may choose to resize
            myBarcode.ResizeTo(400, 100);

            // Save our newly-created barcode as an image
            myBarcode.SaveAsImage("EAN8.jpeg");

            Image myBarcodeImage = myBarcode.Image; // Can be used as Image
            Bitmap myBarcodeBitmap = myBarcode.ToBitmap(); // Can be used as Bitmap

        }
    }
}
