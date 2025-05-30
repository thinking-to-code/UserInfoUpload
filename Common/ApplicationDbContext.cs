﻿using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Common
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<DrivingLicenseImage> DrivingLicenseImages { get; set; }
        public DbSet<RealIdImage> RealIdImages { get; set; }
        public DbSet<DrivingLicenseInfo> DrivingLicenseInfos { get; set; }
        public DbSet<UserAttemptHistory> UserAttemptHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");
        }
    }
}
