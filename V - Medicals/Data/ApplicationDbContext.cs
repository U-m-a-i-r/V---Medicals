using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Models;

namespace V___Medicals.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<string>, string>
    {
        // private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // _httpContextAccessor = httpContextAccessor;
        }

       // public DbSet<Role> Roles { get; set; }
        public override DbSet<User>  Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<DoctorDocument> DoctorDocuments { get; set; }
        public DbSet<PatientDocument> PatientDocuments { get; set; }
        public DbSet<DoctorClinic> DoctorClinics { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Clinic> Clinic { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentDocument> AppointmentDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<User>()
                .HasMany(p => p.Roles)
                .WithOne().HasForeignKey(p => p.UserId).IsRequired();
            modelBuilder.Entity<DoctorClinic>().HasKey(sc => new { sc.DoctorId, sc.ClinicId });
            /*modelBuilder.Entity<Patient>()
                .Property(e=>e.User).IsRequired();
            modelBuilder.Entity<Doctor>()
                .Property(e => e.User).IsRequired();*/

        }

        

    }
}