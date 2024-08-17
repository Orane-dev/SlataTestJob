using Microsoft.EntityFrameworkCore;
using SlataTestJob.DAL.Models;

namespace SlataTestJob.DAL.Implementation
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<TestJob> TestJobs { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        { 
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasData(
                    new Employee { Id = 1, Fullname = "Test IT Director", JobPosition = "IT Director", Department = "IT"},
                    new Employee { Id = 2, Fullname = "Test HR", JobPosition = "HR", Department = "HR department"},
                    new Employee { Id = 3, Fullname = "Test Marketing Director", JobPosition = "Marketing Director", Department = "Marketing"},
                    new Employee { Id = 4, Fullname = "Test Assortment Director", JobPosition = "Assortment Director", Department = "Assortiment"}
                );
        }
    }
}
