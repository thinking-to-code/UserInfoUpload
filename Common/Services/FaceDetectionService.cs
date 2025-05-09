using Common.Services;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Drawing;
using System.IO;
using System.Text;

namespace Common.Services
{
    public class FaceDetectionService : IFaceDetectionService
    {
        // Path to Haar Cascade file
        private readonly string haarCascadePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HaarCascade", "haarcascade_frontalface_default.xml");
        private readonly IConfiguration _configuration;
        private CascadeClassifier _faceDetector;
        private List<ExcelDataObject> excelData = new List<ExcelDataObject>();

        public FaceDetectionService(IConfiguration configuration)
        {
            _faceDetector = new CascadeClassifier(haarCascadePath);
            _configuration = configuration;
        }

        public List<(string, string)> DetectAndSaveFaces(string imagePath)
        {
            Mat image = CvInvoke.Imread(imagePath, ImreadModes.Color);
            Mat grayImage = new Mat();
            CvInvoke.CvtColor(image, grayImage, ColorConversion.Bgr2Gray);

            System.Drawing.Rectangle[] faces = _faceDetector.DetectMultiScale(grayImage, 1.1, 10, new Size(20, 20), Size.Empty);

            List<(string, string)> faceImagePaths = new List<(string, string)>();

            if (faces.Length > 0)
            {
                string imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/detectedFaces");
                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                for (int i = 0; i < faces.Length; i++)
                {
                    ExcelDataObject excelDataObject = new ExcelDataObject();
                    System.Drawing.Rectangle faceRect = faces[i];

                    try
                    {
                        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        string fileName = $"Face_{i + 1}_{timestamp}.jpg";
                        string filePath = Path.Combine(imageDirectory, fileName);

                        using (Mat faceImage = new Mat(image, faceRect))
                        {
                            faceImage.Save(filePath);                            
                        }

                        faceImagePaths.Add(($"/images/detectedFaces/{fileName}", filePath));

                        excelDataObject.Id = i;
                        excelDataObject.ImagePath = imagePath;
                        excelDataObject.SavedFacePath = filePath;
                        excelDataObject.DetectedFaceCount = faces.Length;
                        excelDataObject.Vendor = "Emgu.CV";

                        excelData.Add(excelDataObject);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error saving face image: {ex.Message}");
                        File.AppendAllText("errorLog.txt", $"{DateTime.Now}: {ex.Message}{Environment.NewLine}");
                    }
                }
            }

            return faceImagePaths;
        }

        public async Task<string> MatchFaces(string image1Path, string image2Path)
        {
            try
            {
                var apiUrl = _configuration["AuthenticID:MatchAPIUrl"]; // Replace with the AuthenticID API endpoint.                        
                var accountAccessKey = _configuration["AuthenticID:AccountAccessKey"];
                var secretToken = _configuration["AuthenticID:SecretToken"];

                // 1. Read Image Files
                var image1PathComplete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image1Path);
                var image2PathComplete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image2Path);
                if (!File.Exists(image1PathComplete) || !File.Exists(image2PathComplete))
                {
                    throw new FileNotFoundException("One or both image files do not exist.");
                }

                byte[] image1Bytes = File.ReadAllBytes(image1PathComplete);
                byte[] image2Bytes = File.ReadAllBytes(image2PathComplete);

                // 2. Base64 Encoding
                string image1Base64 = Convert.ToBase64String(image1Bytes);
                string image2Base64 = Convert.ToBase64String(image2Bytes);

                // 3. JSON Payload
                var payload = new
                {
                    image1 = image1Base64,
                    image2 = image2Base64
                };

                string jsonPayload = JsonConvert.SerializeObject(payload);

                // 4. Send the Request with Headers
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("AccountAccessKey", accountAccessKey);
                    client.DefaultRequestHeaders.Add("SecretToken", secretToken);

                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        return responseContent; // Return the API response
                    }
                    else
                    {
                        return $"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }

        public async Task RunFaceMatch()
        {
            string image1Path = "path/to/your/image1.jpg"; // Replace with your image paths
            string image2Path = "path/to/your/image2.jpg";
            var apiUrl = _configuration["AuthenticID:MatchAPIUrl"]; // Replace with the AuthenticID API endpoint.                        
            var accountAccessKey = _configuration["AuthenticID:AccountAccessKey"];
            var secretToken = _configuration["AuthenticID:SecretToken"];

            //string result = await MatchFaces(image1Path, image2Path, apiUrl, accountAccessKey, secretToken);
            //Console.WriteLine(result);
        }
    }
}