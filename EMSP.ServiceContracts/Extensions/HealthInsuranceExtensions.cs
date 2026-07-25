using EMSP.Entities.Models;
using EMSP.ServiceContracts.DTOs.HealthInsuranceDTOs;

namespace EMSP.ServiceContracts.Extensions;

public static class HealthInsuranceExtensions
{
    // Convert Add Request to health object
    public static HealthInsurance ToHealthInsuranceObject(this HealthInsuranceAddRequest healthInsuranceAddRequest)
    {
        return new HealthInsurance()
        {
            PolicyNumber = healthInsuranceAddRequest.PolicyNumber,
            InsuranceProvider = healthInsuranceAddRequest.InsuranceProvider,
            PolicyExpiryDate = healthInsuranceAddRequest.PolicyExpiryDate,
            EstablishmentId = healthInsuranceAddRequest.EstablishmentId
        };
    }
    
    // Convert health to summary response object
    public static HealthInsuranceSummaryResponse ToHealthInsuranceSummaryResponseObject(
        this HealthInsurance healthInsurance)
    {
        return new HealthInsuranceSummaryResponse()
        {
            Id = healthInsurance.Id,
            PolicyNumber = healthInsurance.PolicyNumber,
            InsuranceProvider = healthInsurance.InsuranceProvider,
            PolicyExpiryDate = healthInsurance.PolicyExpiryDate
        };
    }
    
    // Convert health to detailed response object
    public static HealthInsuranceDetailedResponse ToHealthInsuranceDetailedResponseObject(
        this HealthInsurance healthInsurance)
    {
        return new HealthInsuranceDetailedResponse()
        {
            Id = healthInsurance.Id,
            UpdatedAt = healthInsurance.UpdatedAt,
            CreatedAt = healthInsurance.CreatedAt,
            UpdatedBy = healthInsurance.UpdatedBy,
            CreatedBy = healthInsurance.CreatedBy,
            
            PolicyNumber = healthInsurance.PolicyNumber,
            InsuranceProvider = healthInsurance.InsuranceProvider,
            PolicyExpiryDate = healthInsurance.PolicyExpiryDate,
            
            // dto
            Establishment = healthInsurance.Establishment?.ToEstablishmentSummaryResponseObject(),
            
            // list
            Employees = healthInsurance.Employees?.Select(e => e.ToEmployeeSummaryResponseObject()).ToList()
        };
    }
}