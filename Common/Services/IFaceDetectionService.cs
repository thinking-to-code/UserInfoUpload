using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public interface IFaceDetectionService
    {
        List<(string, string)> DetectAndSaveFaces(string imagePath);

        Task<string> MatchFaces(string image1Path, string image2Path);
    }
}
