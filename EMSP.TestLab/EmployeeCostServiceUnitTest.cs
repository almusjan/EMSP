using AutoFixture;
using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using EMSP.ServiceContracts.DTOs.EmployeeCostDTOs;
using EMSP.ServiceContracts.Extensions;
using EMSP.ServiceContracts.Interfaces;
using EMSP.Services;
using FluentAssertions;
using Moq;

namespace EMSP.TestLab;

public class EmployeeCostServiceUnitTest
{
    #region fields
    private readonly IEmployeeCostService _employeeCostService;
    private readonly Mock<IEmployeeCostRepository> _mockEmployeeCostRepository;
    private readonly IFixture _fixture;
    
    #endregion
    
    public EmployeeCostServiceUnitTest()
    {
        _fixture = new Fixture();
        
        _mockEmployeeCostRepository = new Mock<IEmployeeCostRepository>();
        _employeeCostService = new EmployeeCostService(_mockEmployeeCostRepository.Object);
    }

    #region AddEmployeeCost

    [Fact]
    public async Task AddEmployeeCost_NullAddRequest_ThrowsArgumentNullException()
    {
        EmployeeCostAddRequest?  employeeCostAddRequest = null;
        
        Func<Task> action = async () => await _employeeCostService.AddEmployeeCost(employeeCostAddRequest);
        
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task AddEmployeeCost_ValidAddRequest_ToBeSuccess()
    {
        EmployeeCostAddRequest employeeCostAddRequest = _fixture.Create<EmployeeCostAddRequest>();

        EmployeeCost employeeCost = employeeCostAddRequest.ToEmployeeCostObject();
        
        EmployeeCostResponse expectedResponse = employeeCost.ToEmployeeCostResponseObject();
 
        _mockEmployeeCostRepository.Setup(temp => temp.AddAsync(It.IsAny<EmployeeCost>()))
            .ReturnsAsync(employeeCost);
        
        EmployeeCostResponse actualResponse = await _employeeCostService.AddEmployeeCost(employeeCostAddRequest);
        expectedResponse.Id = actualResponse.Id;

        actualResponse.Id.Should().NotBe(Guid.Empty);
        actualResponse.Should().Be(expectedResponse);
    }

    #endregion
    
    #region GetEmployeeCostById

    [Fact]
    public async Task GetEmployeeCostById_NullEmployeeCostId_ThrowsArgumentNullException()
    {
        Guid? id =  null;
        
        Func<Task> action = async () => await _employeeCostService.GetEmployeeCostById(id);
        
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task GetEmployeeCostById_NullEmployeeCostOrSoftDeleted_ThrowsKeyNotFoundException()
    {
        EmployeeCost employeeCost = _fixture.Build<EmployeeCost>()
            .With(ec => ec.Employee, null as Employee)
            .With(ec => ec.IsDeleted, true).Create();

        Func<Task> action = async () =>
        {
            await _employeeCostService.GetEmployeeCostById(employeeCost.Id);
        };
        
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task GetEmployeeCostById_ValidEmployeeCostId_ToBeSuccess()
    {
        EmployeeCost employeeCost = _fixture.Build<EmployeeCost>()
            .With(ec => ec.Employee, null as Employee)
            .With(ec => ec.IsDeleted, false).Create();

        EmployeeCostResponse expectedResponse = employeeCost.ToEmployeeCostResponseObject();
        
        _mockEmployeeCostRepository.Setup(temp => temp.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(employeeCost);
        
        EmployeeCostResponse? actualResponse = await _employeeCostService.GetEmployeeCostById(employeeCost.Id);
        
        actualResponse.Should().Be(expectedResponse);
    }

    #endregion

    #region UpdateEmployeeCost

    [Fact]
    public async Task UpdateEmployeeCost_NullUpdateRequest_ThrowsArgumentNullException()
    {
        EmployeeCostUpdateRequest? employeeCostUpdateRequest =  null;

        Func<Task> action = async () => await _employeeCostService.UpdateEmployeeCost(employeeCostUpdateRequest);
        
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdateEmployeeCost_NonExistId_ThrowsKeyNotFoundException()
    {
        EmployeeCostUpdateRequest? employeeCostUpdateRequest = _fixture.Create<EmployeeCostUpdateRequest>();
        
        Func<Task> action = async () => await _employeeCostService.UpdateEmployeeCost(employeeCostUpdateRequest);
        
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task UpdateEmployeeCost_ValidUpdateRequest_ToBeSuccess()
    {
        EmployeeCost employeeCost = _fixture.Build<EmployeeCost>()
            .With(ec => ec.Employee, null as Employee)
            .With(ec => ec.IsDeleted, false).Create();

        EmployeeCostResponse expectedResponse = employeeCost.ToEmployeeCostResponseObject();

        EmployeeCostUpdateRequest employeeCostUpdateRequest = expectedResponse.ToEmployeeCostUpdateRequest();
        
        _mockEmployeeCostRepository.Setup(temp => temp.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(employeeCost);
        _mockEmployeeCostRepository.Setup(temp => temp.UpdateAsync(It.IsAny<EmployeeCost>()))
            .ReturnsAsync(employeeCost);
        
        EmployeeCostResponse actualResponse = await _employeeCostService.UpdateEmployeeCost(employeeCostUpdateRequest);
        
        actualResponse.Should().Be(expectedResponse);
    }

    #endregion

    #region SoftDeleteEmployeeCost

    [Fact]
    public async Task SoftDeleteEmployeeCost_NullEmployeeCost_ThrowsKeyNotFoundException()
    {
        EmployeeCost employeeCost = _fixture.Build<EmployeeCost>()
            .With(ec => ec.Employee, null as Employee)
            .With(ec => ec.IsDeleted, false).Create();
        
        Func<Task> action = async () => await _employeeCostService.SoftDeleteEmployeeCost(employeeCost.Id);
        
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task SoftDeleteEmployeeCost_EmployeeCostIsSoftDeleted_ThrowsInvalidOperationException()
    {
        EmployeeCost employeeCost = _fixture.Build<EmployeeCost>()
            .With(ec => ec.Employee, null as Employee)
            .With(ec => ec.IsDeleted, true).Create();
        
        _mockEmployeeCostRepository.Setup(temp => temp.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(employeeCost);
        
        Func<Task> action = async () => await _employeeCostService.SoftDeleteEmployeeCost(employeeCost.Id);
        
        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    #endregion
}