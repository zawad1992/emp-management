using Microsoft.EntityFrameworkCore;
using HRMWeb.Models;

namespace HRMWeb.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>()
            .HasKey(e => e.EmployeeId);

        modelBuilder.Entity<Employee>()
            .Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Employee>()
            .Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Division)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Building)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Room)
            .IsRequired()
            .HasMaxLength(20);
    }
}