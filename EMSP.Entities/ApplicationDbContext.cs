using EMSP.Entities.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace EMSP.Entities;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Bank> Banks {get; set;}
    public DbSet<Company> Companies {get; set;}
    public DbSet<Country> Countries {get; set;}
    public DbSet<Employee> Employees {get; set;}
    public DbSet<EmployeeCost> EmployeeCosts {get; set;}
    public DbSet<Establishment> Establishments {get; set;}
    public DbSet<HealthInsurance> HealthInsurances {get; set;}
    public DbSet<Salary> Salaries {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // create tables
        modelBuilder.Entity<Employee>().ToTable("Employees");
        modelBuilder.Entity<EmployeeCost>().ToTable("EmployeeCosts");
        modelBuilder.Entity<Establishment>().ToTable("Establishments");
        modelBuilder.Entity<Company>().ToTable("Companies");
        modelBuilder.Entity<Bank>().ToTable("Banks");
        modelBuilder.Entity<Country>().ToTable("Countries");
        modelBuilder.Entity<HealthInsurance>().ToTable("HealthInsurances");
        modelBuilder.Entity<Salary>().ToTable("Salaries");
        // end
        
        
        // HasPrecision(18,2) to make the max number 16n.2n => 1000000000000000.99 for salary
        modelBuilder.Entity<Salary>()
            .Property(s => s.BasicSalary).HasPrecision(18, 2);
        modelBuilder.Entity<Salary>()
            .Property(s => s.HousingAllowance).HasPrecision(18, 2);
        modelBuilder.Entity<Salary>()
            .Property(s => s.OtherAllowance).HasPrecision(18, 2);
        modelBuilder.Entity<Salary>()
            .Property(s => s.TransportationAllowance).HasPrecision(18, 2);
        modelBuilder.Entity<Salary>()
            .Property(s => s.TotalSalary).HasPrecision(18, 2);
        modelBuilder.Entity<EmployeeCost>()
            .Property(ec => ec.CostAmount ).HasPrecision(18, 2);
        // HasPrecision - end
        
        // convert enum to string
        modelBuilder.Entity<Employee>().Property(e => e.Gender).HasConversion<string>();
        modelBuilder.Entity<Employee>().Property(e => e.Status).HasConversion<string>();
        modelBuilder.Entity<EmployeeCost>().Property(ec => ec.CostType).HasConversion<string>();
        // conversion - end
        
        // model relationships
        modelBuilder.Entity<Employee>().HasOne(e => e.Salary)
            .WithOne().HasForeignKey<Employee>( e=> e.SalaryId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Employee>().HasOne(e => e.Country)
            .WithMany(c => c.Residents).HasForeignKey(e => e.CountryId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Employee>().HasOne(e => e.HealthInsurance)
            .WithMany(hi => hi.Employees).HasForeignKey(e => e.HealthInsuranceId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Employee>().HasOne(e => e.Bank)
            .WithMany(b => b.Employees).HasForeignKey(e => e.BankId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Employee>().HasOne(e => e.Establishment)
            .WithMany(e => e.Employees).HasForeignKey(e => e.EstablishmentId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Employee>().HasOne(e => e.Company)
            .WithMany(c => c.Employees).HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Employee>().HasMany(e => e.EmployeeCosts)
            .WithOne(ec => ec.Employee).HasForeignKey(e => e.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
        // -------
        modelBuilder.Entity<Establishment>().HasMany(e => e.HealthInsurances)
            .WithOne(hi => hi.Establishment).HasForeignKey(hi => hi.EstablishmentId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Establishment>().HasMany(e => e.Companies)
            .WithOne(c => c.Establishment).HasForeignKey(c => c.EstablishmentId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // relationships - end
        
        // indexes
        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.IqamaOrIdNumber).IsUnique();

        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.EmailAddress);

        modelBuilder.Entity<Company>()
            .HasIndex(c => c.CompanyCode);

        modelBuilder.Entity<Establishment>()
            .HasIndex(e => e.EstablishmentCode);
        // indexes - end
    }
}