namespace EMSP.Entities.Models;

public class Employee : BaseEntity
{
    #region Personal Info

    public string? FullNameAr { get; set; }
    public string? FullNameEn { get; set; }
    public string? IqamaOrIdNumber {get; set;}
    public string? PassportNumber {get; set;} // Non-Saudi
    public string? BorderNumber {get; set;} // Non-Saudi
    public DateTime? DateOfBirth {get; set;}
    public string? Gender {get; set;}
    public string? EmailAddress {get; set;}
    public string? PhoneNumber {get; set;}
    
    // Expiry Dates
    public DateTime? PassportExpiryDate {get; set;} // Non-Saudi
    public DateTime? IqamaOrIdExpiryDate {get; set;}
    
    // FK
    public Guid? CountryId {get; set;}
    public virtual Country? Country {get; set;}
    
    #endregion

    #region Employment Info

    public string? Profession {get; set;}
    public string? ContractNumber {get; set;}
    public string? EmployeeStatus { get; set; }
    public DateTime? HireDate {get; set;}
    public DateTime? TerminationDate {get; set;}
    public bool? IsTerminated { get; set; } = false;
    
    // FKs
     public Guid? EstablishmentId {get; set;}
     public virtual Establishment? Establishment {get; set;}
     
     public Guid? CompanyId {get; set;}
     public virtual Company? Company {get; set;}
     
    #endregion

    #region Bank & Salary Infos

    public string? Iban {get; set;}
    public string? AccountNumber {get; set;}
    
    // only if the employee's bank is not listed
    public string? UnlistedBankName {get; set;} // Non-Saudi
    
    // FKs
    public Guid? BankId {get; set;}
    public virtual Bank?  Bank {get; set;}
    
    public Guid? SalaryId {get; set;}
    public virtual Salary?  Salary {get; set;}

    #endregion

    #region Health Insurance Info

    public string? MemberPolicyNumber {get; set;}

    // FK
    public Guid? HealthInsuranceId {get; set;}
    public virtual HealthInsurance?  HealthInsurance {get; set;}

    #endregion

    #region Costs

    public virtual ICollection<EmployeeCost>?  EmployeeCosts {get; set;}

    #endregion
}