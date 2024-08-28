using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShushrutEyeHospitalCRM.Models.DTO;

namespace ShushrutEyeHospitalCRM.ApplicationContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<Doctor>? Doctors { get; set; }
        public DbSet<Patient>? Patients { get; set; }
        public DbSet<Receptionist>? Receptionists { get; set; }
        public DbSet<Refraction>? Refractions { get; set; }
        public DbSet<PatientAdvGlasses>? PatientAdvGlasses { get; set; }
        public DbSet<PatientExaminationDetail>? ExaminationDetail { get; set; }
        public DbSet<Counsling>? Counsling { get; set; }
        public DbSet<PatientCounseling>? PatientCounseling { get; set; }
        public DbSet<PatientDischarge>? PatientDischarge { get; set; }
        public DbSet<CommonEyeProblem>? CommonEyeProblem { get; set; }
        public DbSet<Medicines>? Medicines { get; set; }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
            base.ConfigureConventions(configurationBuilder);
        }
        public DbSet<ShushrutEyeHospitalCRM.Models.DTO.PatientHistory>? PatientHistory { get; set; }
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasMany(m => m.Doctors).WithOne(s => s.Department)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>().HasMany(m => m.Doctors).WithOne(s => s.ApplicationUser)
              .HasForeignKey(x => x.UserId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>().HasMany(m => m.Patients).WithOne(s => s.ApplicationUser)
              .HasForeignKey(x => x.UserId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>().HasMany(m => m.Receptionists).WithOne(s => s.ApplicationUser)
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>().HasMany(m => m.Refractions).WithOne(s => s.ApplicationUser)
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Patient>().HasOne(s => s.Receptionist).WithOne(y=>y.Patient)
             .HasForeignKey(x => x.Rece)
             .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);

        }*/
    }
}
