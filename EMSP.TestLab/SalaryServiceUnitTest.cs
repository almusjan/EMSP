using AutoFixture;
using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using EMSP.ServiceContracts.DTOs.EstablishmentDTOs;
using EMSP.ServiceContracts.DTOs.SalaryDTOs;
using EMSP.ServiceContracts.Extensions;
using EMSP.ServiceContracts.Interfaces;
using EMSP.Services;
using FluentAssertions;
using Moq;

namespace EMSP.TestLab;

public class SalaryServiceUnitTest
{
     #region fields
    
    private readonly ISalaryService _salaryService;
    private readonly Mock<ISalaryRepository> _mockSalaryRepository;
    private readonly IFixture _fixture;
    
    #endregion
    
    public SalaryServiceUnitTest()
    {
        _fixture = new Fixture();
        
        _mockSalaryRepository = new Mock<ISalaryRepository>();
        _salaryService = new SalaryService(_mockSalaryRepository.Object);
    }

    #region AddSalary

    [Fact]
    public async Task AddSalary_NullAddRequest_ThrowsArgumentNullException()
    {
        SalaryAddRequest?  salaryAddRequest = null;

        Func<Task> action = async () => await _salaryService.AddSalary(salaryAddRequest);
        
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task AddSalary_ValidAddRequest_ToBeSuccess()
    {
        SalaryAddRequest salaryAddRequest = _fixture.Create<SalaryAddRequest>();

        Salary salary = salaryAddRequest.ToSalaryObject();

        SalaryResponse expectedResponse = salary.ToSalaryResponseObject();
 
        _mockSalaryRepository.Setup(temp => temp.AddAsync(It.IsAny<Salary>()))
            .ReturnsAsync(salary);

        SalaryResponse actualResponse =
            await _salaryService.AddSalary(salaryAddRequest);
        expectedResponse.Id = actualResponse.Id;

        actualResponse.Id.Should().NotBe(Guid.Empty);
        actualResponse.Should().Be(expectedResponse);
    }

    #endregion

    #region UpdateSalary

    [Fact]
    public async Task UpdateSalary_NullUpdateRequest_ThrowsArgumentNullException()
    {
        SalaryUpdateRequest? salaryUpdateRequest =  null;

        Func<Task> action = async () => await _salaryService.UpdateSalary(salaryUpdateRequest);
        
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdateSalary_NonExistId_ThrowsKeyNotFoundException()
    {
        SalaryUpdateRequest  salaryUpdateRequest = _fixture.Create<SalaryUpdateRequest>();
        
        Func<Task> action = async () => await _salaryService.UpdateSalary(salaryUpdateRequest);
        
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task UpdateSalary_ValidUpdateRequest_ToBeSuccess()
    {
        Salary salary = _fixture.Create<Salary>();

        SalaryResponse expectedResponse = salary.ToSalaryResponseObject();

        SalaryUpdateRequest salaryUpdateRequest = expectedResponse.ToSalaryUpdateRequest();
        
        _mockSalaryRepository.Setup(temp => temp.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(salary);
        _mockSalaryRepository.Setup(temp => temp.UpdateAsync(It.IsAny<Salary>()))
            .ReturnsAsync(salary);

        SalaryResponse actualResponse =
            await _salaryService.UpdateSalary(salaryUpdateRequest);
        
        actualResponse.Should().Be(expectedResponse);
    }

    #endregion
}