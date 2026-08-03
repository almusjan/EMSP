using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using EMSP.ServiceContracts.DTOs.EmployeeCostDTOs;
using EMSP.ServiceContracts.Extensions;
using EMSP.ServiceContracts.Interfaces;
using EMSP.Services.Helpers;

namespace EMSP.Services;

public class EmployeeCostService : IEmployeeCostService
{
    private readonly IEmployeeCostRepository _employeeCostRepository;

    public EmployeeCostService(IEmployeeCostRepository employeeCostRepository)
    {
        _employeeCostRepository = employeeCostRepository;
    }
    
    public async Task<EmployeeCostResponse> AddEmployeeCost(EmployeeCostAddRequest? employeeCostAddRequest)
    {
        if(employeeCostAddRequest == null)
            throw new  ArgumentNullException(nameof(employeeCostAddRequest));
        
        ValidationHelper.ModelValidation(employeeCostAddRequest);

        EmployeeCost employeeCost = employeeCostAddRequest.ToEmployeeCostObject();
        employeeCost.Id =  Guid.NewGuid();
        
        await _employeeCostRepository.AddAsync(employeeCost);

        return employeeCost.ToEmployeeCostResponseObject();
    }

    public async Task<EmployeeCostResponse?> GetEmployeeCostById(Guid? employeeCostId)
    {
        if (employeeCostId == null)
            throw new ArgumentNullException(nameof(employeeCostId));
        
        EmployeeCost?  employeeCost = await _employeeCostRepository.GetByIdAsync(employeeCostId);

        if (employeeCost == null || employeeCost.IsDeleted)
            throw new KeyNotFoundException($"The employee cost with ID {employeeCostId} not found or soft-deleted");
        
        return employeeCost.ToEmployeeCostResponseObject();
    }

    public async Task<EmployeeCostResponse> UpdateEmployeeCost(EmployeeCostUpdateRequest? employeeCostUpdateRequest)
    {
        if (employeeCostUpdateRequest == null)
            throw new ArgumentNullException(nameof(employeeCostUpdateRequest));
        
        ValidationHelper.ModelValidation(employeeCostUpdateRequest);
        
        EmployeeCost? matchingEmployeeCost = await  _employeeCostRepository.GetByIdAsync(employeeCostUpdateRequest.Id);
        
        if (matchingEmployeeCost == null)
            throw new KeyNotFoundException($"The employee cost with ID {employeeCostUpdateRequest.Id} not found");

        #region CheckingUpdateFields

        matchingEmployeeCost.CostType = employeeCostUpdateRequest.CostType;
        matchingEmployeeCost.CostAmount =  employeeCostUpdateRequest.CostAmount;
        matchingEmployeeCost.Description =  employeeCostUpdateRequest.Description;
        matchingEmployeeCost.DueDate = employeeCostUpdateRequest.DueDate;
        matchingEmployeeCost.IsPaid = employeeCostUpdateRequest.IsPaid;
        if(matchingEmployeeCost.IsPaid)
            matchingEmployeeCost.PaidDate = employeeCostUpdateRequest.PaidDate;
        matchingEmployeeCost.ReferenceNumber =  employeeCostUpdateRequest.ReferenceNumber;

        #endregion
        
        
        await  _employeeCostRepository.UpdateAsync(matchingEmployeeCost);
        
        return matchingEmployeeCost.ToEmployeeCostResponseObject();
    }

    public async Task SoftDeleteEmployeeCost(Guid employeeCostId)
    {
        EmployeeCost? employeeCost = await _employeeCostRepository.GetByIdAsync(employeeCostId);
        
        if(employeeCost == null)
            throw new KeyNotFoundException($"The employee cost with ID {employeeCostId} not found");
        
        if(employeeCost.IsDeleted)
            throw new InvalidOperationException("The employee cost is already soft-deleted");
        
        employeeCost.IsDeleted = true;
        await _employeeCostRepository.UpdateAsync(employeeCost);
    }
}