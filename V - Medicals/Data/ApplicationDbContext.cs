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
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Speciality> Specialities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
            /*modelBuilder.Entity<Patient>()
                .Property(e=>e.User).IsRequired();
            modelBuilder.Entity<Doctor>()
                .Property(e => e.User).IsRequired();*/
        }
    }
}