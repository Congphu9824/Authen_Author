using Data;
using Microsoft.EntityFrameworkCore;

namespace API.AppliDbContext
{
    public class EmployDbContext : DbContext
    {
        public EmployDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-TUM2DU4\\SQLEXPRESS01;Database=Practice_Blazor;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                    new Employee
                    {
                        Id = 1,
                        Name = "Thao",
                        Email = "thao@example.com",
                        Location = "Hanoi",
                        Salary = 50000.00m,
                        Address = "123 Main St, Hanoi",
                        Gender = "Female",
                        Designation = "Software Engineer",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        Status = "Active"
                    },
                    new Employee
                    {
                         Id = 2,
                         Name = "Quân",
                         Email = "Quan@example.com",
                         Location = "Yen bai",
                         Salary = 40000.00m,
                         Address = "123 Main St, Hanoi",
                         Gender = "Male",
                         Designation = "Software Engineer",
                         DateOfBirth = new DateTime(2004, 1, 1),
                         Status = "InActive"
                    }
                    );

        }
    }
}
