using Microsoft.EntityFrameworkCore;
using UserInfoUpload.Models;

namespace UserInfoUpload.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<DrivingLicenseImage> DrivingLicenseImages { get; set; }
        public DbSet<DrivingLicenseInfo> DrivingLicenseInfos { get; set; }
    }
}
