using System.Linq.Expressions;
using Xunit.Abstractions;
using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using EMSP.ServiceContracts.Interfaces;
using EMSP.Services;
using AutoFixture;
using EMSP.Entities.Enums;
using EMSP.ServiceContracts.DTOs.EmployeeDTOs;
using EMSP.ServiceContracts.Extensions;
using FluentAssertions;
using Moq;

namespace EMSP.TestLab;

public class EmployeeServiceUnitTest
{
    #region fields

    private readonly IFixture _fixture;
    private readonly ITestOutputHelper _outputHelper;
    private readonly  IEmployeeService _employeeService;
    private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
    
    #endregion
    
    public EmployeeServiceUnitTest(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _fixture = new Fixture();
        
        _mockEmployeeRepository = new Mock<IEmployeeRepository>();
        _employeeService = new EmployeeService(_mockEmployeeRepository.Object);
    }

    #region GetEmployees

    [Fact]
    public async Task GetEmployees_EmptyList()
    {
        // Arrange
        List<Employee> employees = new List<Employee>();
        
        _mockEmployeeRepository.Setup(temp => temp.GetAllAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
            .ReturnsAsync(employees);
        
        // Act
        List<EmployeeSummaryResponse>? employeeResponses = await _employeeService.GetEmployees();

        // Assert
        employeeResponses.Should().BeEmpty();
    }

    [Fact]
    public async Task GetEmployees_WithStatusFilter_Successful()
    {
        // Arrange
        List<Employee> employees =
        [
            _fixture.Build<Employee>()
                .With(e => e.Status, EmployeeStatus.Active)
                .With(e => e.Country, null as Country).With(e => e.Establishment, null as Establishment)
                .With(e => e.Company, null as Company).With(e => e.Salary, null as Salary)
                .With(e => e.Bank, null as Bank).With(e => e.HealthInsurance, null as HealthInsurance)
                .With(e => e.EmployeeCosts, null as List<EmployeeCost>)
                .Create(),
            _fixture.Build<Employee>()
                .With(e => e.Status, EmployeeStatus.Active)
                .With(e => e.Country, null as Country).With(e => e.Establishment, null as Establishment)
                .With(e => e.Company, null as Company).With(e => e.Salary, null as Salary)
                .With(e => e.Bank, null as Bank).With(e => e.HealthInsurance, null as HealthInsurance)
                .With(e => e.EmployeeCosts, null as List<EmployeeCost>)
                .Create(),
            _fixture.Build<Employee>()
                .With(e => e.Status, EmployeeStatus.Active)
                .With(e => e.Country, null as Country).With(e => e.Establishment, null as Establishment)
                .With(e => e.Company, null as Company).With(e => e.Salary, null as Salary)
                .With(e => e.Bank, null as Bank).With(e => e.HealthInsurance, null as HealthInsurance)
                .With(e => e.EmployeeCosts, null as List<EmployeeCost>)
                .Create(),
        ];

        List<EmployeeSummaryResponse> expectedEmployeeResponses =
            employees.Select(e => e.ToEmployeeSummaryResponseObject()).ToList();
        _outputHelper.WriteLine("Expected:");
        foreach (EmployeeSummaryResponse expectedResponse in expectedEmployeeResponses)
        {
            _outputHelper.WriteLine(expectedResponse.ToString());
        }
        
        // Act
        _mockEmployeeRepository.Setup(temp => temp.GetAllAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
            .ReturnsAsync(employees);
        
        List<EmployeeSummaryResponse>? actualEmployeeResponses = await _employeeService.GetEmployees(EmployeeStatus.Active);
        _outputHelper.WriteLine("Actual:");
        foreach (EmployeeSummaryResponse actualResponse in actualEmployeeResponses)
        {
            _outputHelper.WriteLine(actualResponse.ToString());
        }
        
        // Assert
        actualEmployeeResponses.Should().BeEquivalentTo(expectedEmployeeResponses);
    }

    #endregion

    #region GetFilteredEmployees

    [Fact]
    public async Task GetFilteredEmployees_EmptySearchString_Successful()
    {
        // Arrange
        List<Employee> employees =
        [
            _fixture.Build<Employee>().With(e => e.Country, null as Country).With(e => e.Establishment, null as Establishment)
                .With(e => e.Company, null as Company).With(e => e.Salary, null as Salary)
                .With(e => e.Bank, null as Bank).With(e => e.HealthInsurance, null as HealthInsurance)
                .With(e => e.EmployeeCosts, null as List<EmployeeCost>)
                .Create(),
            _fixture.Build<Employee>().With(e => e.Country, null as Country).With(e => e.Establishment, null as Establishment)
                .With(e => e.Company, null as Company).With(e => e.Salary, null as Salary)
                .With(e => e.Bank, null as Bank).With(e => e.HealthInsurance, null as HealthInsurance)
                .With(e => e.EmployeeCosts, null as List<EmployeeCost>)
                .Create(),
            _fixture.Build<Employee>().With(e => e.Country, null as Country).With(e => e.Establishment, null as Establishment)
                .With(e => e.Company, null as Company).With(e => e.Salary, null as Salary)
                .With(e => e.Bank, null as Bank).With(e => e.HealthInsurance, null as HealthInsurance)
                .With(e => e.EmployeeCosts, null as List<EmployeeCost>)
                .Create(),
        ];

        List<EmployeeSummaryResponse> expectedEmployeeResponses =
            employees.Select(e => e.ToEmployeeSummaryResponseObject()).ToList();
        _outputHelper.WriteLine("Expected:");
        foreach (EmployeeSummaryResponse expectedResponse in expectedEmployeeResponses)
        {
            _outputHelper.WriteLine(expectedResponse.ToString());
        }
        
        // Act
        _mockEmployeeRepository.Setup(temp => temp.GetAllAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
            .ReturnsAsync(employees);
        
        List<EmployeeSummaryResponse>? actualEmployeeResponses =
            await _employeeService.GetFilteredEmployees(nameof(Employee.IqamaOrIdNumber), "");
        _outputHelper.WriteLine("Actual:");
        foreach (EmployeeSummaryResponse actualResponse in actualEmployeeResponses)
        {
            _outputHelper.WriteLine(actualResponse.ToString());
        }
        
        // Assert
        actualEmployeeResponses.Should().BeEquivalentTo(expectedEmployeeResponses);
    }

    [Fact]
    public async Task GetFilteredEmployees_FilterByIqama_Successful()
    {
        // Arrange
        List<Employee> employees =
        [
            _fixture.Build<Employee>().With(e => e.Country, null as Country).With(e => e.Establishment, null as Establishment)
                .With(e => e.Company, null as Company).With(e => e.Salary, null as Salary)
                .With(e => e.Bank, null as Bank).With(e => e.HealthInsurance, null as HealthInsurance)
                .With(e => e.EmployeeCosts, null as List<EmployeeCost>)
                .Create(),
            _fixture.Build<Employee>().With(e => e.Country, null as Country).With(e => e.Establishment, null as Establishment)
                .With(e => e.Company, null as Company).With(e => e.Salary, null as Salary)
                .With(e => e.Bank, null as Bank).With(e => e.HealthInsurance, null as HealthInsurance)
                .With(e => e.EmployeeCosts, null as List<EmployeeCost>)
                .Create(),
            _fixture.Build<Employee>().With(e => e.Country, null as Country).With(e => e.Establishment, null as Establishment)
                .With(e => e.Company, null as Company).With(e => e.Salary, null as Salary)
                .With(e => e.Bank, null as Bank).With(e => e.HealthInsurance, null as HealthInsurance)
                .With(e => e.EmployeeCosts, null as List<EmployeeCost>)
                .Create(),
        ];

        List<EmployeeSummaryResponse> expectedEmployeeResponses =
            employees.Select(e => e.ToEmployeeSummaryResponseObject()).ToList();
        _outputHelper.WriteLine("Expected:");
        foreach (EmployeeSummaryResponse expectedResponse in expectedEmployeeResponses)
        {
            _outputHelper.WriteLine(expectedResponse.ToString());
        }
        
        // Act
        _mockEmployeeRepository.Setup(temp => temp.GetAllAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
            .ReturnsAsync(employees);
        
        List<EmployeeSummaryResponse>? actualEmployeeResponses =
            await _employeeService.GetFilteredEmployees(nameof(Employee.IqamaOrIdNumber), "2135468792");
        _outputHelper.WriteLine("Actual:");
        foreach (EmployeeSummaryResponse actualResponse in actualEmployeeResponses)
        {
            _outputHelper.WriteLine(actualResponse.ToString());
        }
        
        // Assert
        actualEmployeeResponses.Should().BeEquivalentTo(expectedEmployeeResponses);
    }

    #endregion

    #region AddEmployee

    [Fact]
    public async Task AddEmployee_WithNullAddRequestRequest_ThrowsArgumentNullException()
    {
        // Arrange
        EmployeeAddRequest? employeeAddRequest = null;
        
        // Act
        Func<Task> action = async () =>
        {
            await _employeeService.AddEmployee(employeeAddRequest);
        };
        
        // Assert
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task AddEmployee_DuplicatedIqamaNumber_ThrowsInvalidOperationException()
    {
        // Arrange
        EmployeeAddRequest employeeAddRequest1 = _fixture.Build<EmployeeAddRequest>()
            .With(e => e.IqamaOrIdNumber, "2013465798")
            .Create();
        EmployeeAddRequest employeeAddRequest2 = _fixture.Build<EmployeeAddRequest>()
            .With(e => e.IqamaOrIdNumber, "2013465798")
            .Create();

        // Act
        Employee firstEmployee = employeeAddRequest1.ToEmployeeObject();

        _mockEmployeeRepository.Setup(temp => temp.IsIqamaExistsAsync(It.IsAny<string>()))
            .ReturnsAsync(false);
        _mockEmployeeRepository.Setup(temp => temp.AddAsync(It.IsAny<Employee>()))
            .ReturnsAsync(firstEmployee);
        await _employeeService.AddEmployee(employeeAddRequest1);

        Func<Task> action = async () =>
        {
            _mockEmployeeRepository.Setup(temp => temp.IsIqamaExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);
            _mockEmployeeRepository.Setup(temp => temp.AddAsync(It.IsAny<Employee>()))
                .ReturnsAsync(firstEmployee);
            await _employeeService.AddEmployee(employeeAddRequest2);
        };
        
        // Assert
        await action.Should().ThrowAsync<InvalidOperationException>();
        
    }


    [Fact]
    public async Task AddEmployee_ProperEmployeeData_Successful()
    {
        // Arrange
        EmployeeAddRequest employeeAddRequest = _fixture.Build<EmployeeAddRequest>()
            .With(e => e.FullNameAr, "قصي عباس")
            .With(e => e.FullNameEn, "Qusay Abbas")
            .With(e => e.IqamaOrIdNumber, "2013465798")
            .Create();
        
        Employee employee =  employeeAddRequest.ToEmployeeObject();

        EmployeeSummaryResponse expectedEmployeeResponse = employee.ToEmployeeSummaryResponseObject();
        
        // Act
        _mockEmployeeRepository.Setup(temp => temp.AddAsync(It.IsAny<Employee>()))
            .ReturnsAsync(employee);
        
        EmployeeSummaryResponse actualEmployeeResponse = await _employeeService.AddEmployee(employeeAddRequest);
        expectedEmployeeResponse.Id = actualEmployeeResponse.Id;
        
        // Assert
        actualEmployeeResponse.Id.Should().NotBe(Guid.Empty);
       actualEmployeeResponse.Should().Be(expectedEmployeeResponse);
    }

    #endregion

    #region GetEmployeeById

    [Fact]
    public async Task GetEmployeeById_NullEmployeeId_ThrowsArgumentNullException()
    {
        // Arrange
        Guid? id =  null;
        
        // Act
        Func<Task> action = async () => await _employeeService.GetEmployeeById(id);
        
        // Assert
        await action.Should().ThrowAsync<ArgumentNullException>();
    }
    
    [Fact]
    public async Task GetEmployeeById_NullEmployeeOrSoftDeleted_ThrowsKeyNotFoundException()
    {
        // Arrange
        Employee employee = _fixture.Build<Employee>().With(e => e.Country, null as Country)
            .With(e => e.IsDeleted, true)
            .With(e => e.Establishment, null as Establishment)
            .With(e => e.Company, null as Company).With(e => e.Salary, null as Salary)
            .With(e => e.Bank, null as Bank).With(e => e.HealthInsurance, null as HealthInsurance)
            .With(e => e.EmployeeCosts, null as List<EmployeeCost>)
            .Create();

        // Act
        _mockEmployeeRepository.Setup(temp => temp.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Employee);
        
        // Act
        Func<Task> action = async () => await _employeeService.GetEmployeeById(employee.Id);
        
        // Assert
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task GetEmployeeById_ValidEmployeeId_Successful()
    {
        // Arrange
        Employee employee = _fixture.Build<Employee>().With(e => e.Country, null as Country)
            .With(e => e.IsDeleted, false)
            .With(e => e.Establishment, null as Establishment)
            .With(e => e.Company, null as Company).With(e => e.Salary, null as Salary)
            .With(e => e.Bank, null as Bank).With(e => e.HealthInsurance, null as HealthInsurance)
            .With(e => e.EmployeeCosts, null as List<EmployeeCost>)
            .Create();
        
        EmployeeDetailedResponse expectedResponse =  employee.ToEmployeeDetailedResponseObject();

        // Act
        _mockEmployeeRepository.Setup(temp => temp.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(employee);
        
        EmployeeDetailedResponse? actualResponse = await _employeeService.GetEmployeeById(expectedResponse.Id);
        
        // Assert
        actualResponse.Should().Be(expectedResponse);
    }

    #endregion

    #region UpdateEmployee

    [Fact]
    public async Task UpdateEmployee_NullUpdateRequest_ThrowsArgumentNullException()
    {
        // Arrange
        EmployeeUpdateRequest?  employeeUpdateRequest = null;
        
        // Act
        Func<Task> action = async () =>
        {
            await _employeeService.UpdateEmployee(employeeUpdateRequest);
        };

        // Assert
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdateEmployee_WithNonExistentId_ThrowsKeyNotFoundException()
    {
        // Arrange
        EmployeeUpdateRequest employeeUpdateRequest = _fixture.Create<EmployeeUpdateRequest>();
        
        // Act
        Func<Task> action = async () =>
        {
            await _employeeService.UpdateEmployee(employeeUpdateRequest);
        };
        
        // Assert
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task UpdateEmployee_ValidUpdateRequest_Successful()
    {
        Employee employee = _fixture.Build<Employee>().With(e => e.Country, null as Country)
            .With(e => e.Establishment, null as Establishment)
            .With(e => e.Company, null as Company).With(e => e.Salary, null as Salary)
            .With(e => e.Bank, null as Bank).With(e => e.HealthInsurance, null as HealthInsurance)
            .With(e => e.EmployeeCosts, null as List<EmployeeCost>)
            .Create();
        
        EmployeeSummaryResponse expectedResponse =  employee.ToEmployeeSummaryResponseObject();
        
        EmployeeUpdateRequest updateRequest = expectedResponse.ToEmployeeUpdateRequest();
        
        // Act
        _mockEmployeeRepository.Setup(temp => temp.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(employee);
        _mockEmployeeRepository.Setup(temp => temp.UpdateAsync(It.IsAny<Employee>()))
            .ReturnsAsync(employee);
        
        EmployeeSummaryResponse actualResponse = await _employeeService.UpdateEmployee(updateRequest);
        
        // Assert
        actualResponse.Should().Be(expectedResponse);
    }

    #endregion

    #region SoftDeleteEmployee

    [Fact]
    public async Task SoftDeleteEmployee_NullEmployee_ThrowsKeyNotFoundException()
    {
        // Arrange
        Employee employee = _fixture.Build<Employee>()
            .With(e => e.Country, null as Country)
            .With(e => e.Company, null as Company)
            .With(e => e.Establishment, null as Establishment)
            .With(e => e.Bank, null as Bank)
            .With(e => e.Salary, null as Salary)
            .With(e => e.HealthInsurance, null as HealthInsurance)
            .With(e => e.EmployeeCosts, null as List<EmployeeCost>)
            .Create();
        
        // Act
        Func<Task> action = async () =>
        {
            await _employeeService.SoftDeleteEmployee(employee.Id);
        };
        
        // Assert
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task SoftDeleteEmployee_EmployeeSoftDeleted_ThrowsInvalidOperationException()
    {
        // Arrange
        Employee employee = _fixture.Build<Employee>()
            .With(e => e.Status, EmployeeStatus.Terminated)
            .With(e => e.TerminationDate, DateTime.UtcNow)
            .With(e => e.IsDeleted, true)
            .With(e => e.Country, null as Country)
            .With(e => e.Company, null as Company)
            .With(e => e.Establishment, null as Establishment)
            .With(e => e.Bank, null as Bank)
            .With(e => e.Salary, null as Salary)
            .With(e => e.HealthInsurance, null as HealthInsurance)
            .With(e => e.EmployeeCosts, null as List<EmployeeCost>)
            .Create();
        
        // Act
        Func<Task> action = async () =>
        {
            await _employeeService.SoftDeleteEmployee(employee.Id);
        };
        
        // Assert
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }
    
    [Fact]
    public async Task SoftDeleteEmployee_EmployeeIsActive_ThrowsInvalidOperationException()
    {
        // Arrange
        Employee employee = _fixture.Build<Employee>()
            .With(e => e.Status, EmployeeStatus.Active)
            .With(e => e.Country, null as Country)
            .With(e => e.Company, null as Company)
            .With(e => e.Establishment, null as Establishment)
            .With(e => e.Bank, null as Bank)
            .With(e => e.Salary, null as Salary)
            .With(e => e.HealthInsurance, null as HealthInsurance)
            .With(e => e.EmployeeCosts, null as List<EmployeeCost>)
            .Create();
        
        // Act
        Func<Task> action = async () =>
        {
            await _employeeService.SoftDeleteEmployee(employee.Id);
        };
        
        // Assert
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    #endregion 
}