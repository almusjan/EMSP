using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMSP.Entities.Enums;

namespace EMSP.Entities.Models;

public class Employee : BaseEntity
{
    #region Personal Info
    
    [Required]
    [StringLength(100)]
    public string? FullNameAr { get; set; }
    [Required]
    [StringLength(100)]
    public string? FullNameEn { get; set; }
    [Required]
    [StringLength(10)]
    public string? IqamaOrIdNumber {get; set;}
    [StringLength(100)]
    public string? PassportNumber {get; set;} // Non-Saudi
    [StringLength(15)]
    public string? BorderNumber {get; set;} // Non-Saudi
    public DateTime? DateOfBirth {get; set;}
    public GenderOptions? Gender {get; set;}
    [StringLength(100)]
    public string? EmailAddress {get; set;}
    [StringLength(15)]
    public string? PhoneNumber {get; set;}
    
    // Expiry Dates
    public DateTime? PassportExpiryDate {get; set;} // Non-Saudi
    public DateTime? IqamaOrIdExpiryDate {get; set;}
    
    // FK
    [Required(ErrorMessage = "Country is required")]
    public Guid? CountryId {get; set;}
    [ForeignKey(nameof(CountryId))]
    public virtual Country? Country {get; set;}
    
    #endregion

    #region Employment Info

    [StringLength(100)]
    public string? Profession {get; set;}
    [StringLength(15)]
    public string? ContractNumber {get; set;}
    public EmployeeStatus? Status { get; set; }
    public DateTime? HireDate {get; set;}
    public DateTime? TerminationDate {get; set;}
    
    // FKs
     public Guid? EstablishmentId {get; set;}
     [ForeignKey(nameof(EstablishmentId))]
     public virtual Establishment? Establishment {get; set;}
     
     public Guid? CompanyId {get; set;}
     [ForeignKey(nameof(CompanyId))]
     public virtual Company? Company {get; set;}
     
    #endregion

    #region Bank & Salary Infos

    [StringLength(50)]
    public string? Iban {get; set;}
    [StringLength(50)]
    public string? AccountNumber {get; set;}
    
    // only if the employee's bank is not listed
    [StringLength(50)]
    public string? UnlistedBankName {get; set;} // Non-Saudi
    
    // FKs
    public Guid? BankId {get; set;}
    [ForeignKey(nameof(BankId))]
    public virtual Bank?  Bank {get; set;}
    
    public virtual Salary?  Salary {get; set;}

    #endregion

    #region Health Insurance Info

    [StringLength(50)]
    public string? MemberPolicyNumber {get; set;}

    // FK
    public Guid? HealthInsuranceId {get; set;}
    [ForeignKey(nameof(HealthInsuranceId))]
    public virtual HealthInsurance?  HealthInsurance {get; set;}

    #endregion

    #region Costs

    public virtual ICollection<EmployeeCost>?  EmployeeCosts {get; set;}

    #endregion
}