using EMSP.Entities.Models;
using EMSP.ServiceContracts.DTOs.EmployeeCostDTOs;

namespace EMSP.ServiceContracts.Extensions;

public static class EmployeeCostExtensions
{
    // Convert AddRequest to employee cost object
    public static EmployeeCost ToEmployeeCostObject(this EmployeeCostAddRequest employeeCostAddRequest)
    {
        return new EmployeeCost()
        {
            CostAmount = employeeCostAddRequest.CostAmount,
            CostType = employeeCostAddRequest.CostType.ToString(),
            Description =  employeeCostAddRequest.Description,
            DueDate = employeeCostAddRequest.DueDate,
            EmployeeId = employeeCostAddRequest.EmployeeId,
            ReferenceNumber = employeeCostAddRequest.ReferenceNumber,
        };
    }
    // Convert employee cost to response object
    public static EmployeeCostResponse ToEmployeeCostResponseObject(this EmployeeCost employeeCost)
    {
        return new EmployeeCostResponse()
        {
            Id =  employeeCost.Id,
            CreatedAt =   employeeCost.CreatedAt,
            CreatedBy =  employeeCost.CreatedBy,
            UpdatedAt =   employeeCost.UpdatedAt,
            UpdatedBy =  employeeCost.UpdatedBy,

            CostAmount = employeeCost.CostAmount,
            CostType = employeeCost.CostType,
            Description =  employeeCost.Description,
            DueDate = employeeCost.DueDate,
            IsPaid =  employeeCost.IsPaid,
            PaidDate =  employeeCost.PaidDate,
            ReferenceNumber = employeeCost.ReferenceNumber
        };
    }
}