using Microsoft.EntityFrameworkCore;
using toolbox.Models;

namespace entity_framework
{
    public class MyCompanyContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public string ConnectionString { get; }

        public MyCompanyContext(DbContextOptions<MyCompanyContext> options) : base(options)
        {
            //ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=MyCompany; Integrated Security=true; Trusted_Connection=true;";
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(ConnectionString);
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Project>().ToTable("Projects");

            modelBuilder.Entity<Employee>().HasKey(e => e.EmployeeId);
            modelBuilder.Entity<Department>().HasKey(d => d.DeparmentId);
            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);
            modelBuilder.Entity<Project>().HasKey(p => p.ProjectId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
