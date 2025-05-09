using Common.Services;
using Emgu.CV.Face;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInfoUpload.Services;

namespace Common
{
    public static class RegisterDbServices
    {
        public static void AddDbServices(this IServiceCollection services, string connectionString)
        {
            // Add your DbContext and other services here
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Add other services as needed
            services.AddScoped<IFaceDetectionService ,FaceDetectionService>();
            services.AddScoped<IronBarcodeReaderService>();
        }
    }
}
