using System.Linq.Expressions;
using AutoFixture;
using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using EMSP.ServiceContracts.DTOs.EstablishmentDTOs;
using EMSP.ServiceContracts.DTOs.HealthInsuranceDTOs;
using EMSP.ServiceContracts.Extensions;
using EMSP.ServiceContracts.Interfaces;
using EMSP.Services;
using FluentAssertions;
using Moq;
using Xunit.Abstractions;

namespace EMSP.TestLab;

public class HealthInsuranceServiceUnitTest
{
     #region fields
    
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly IHealthInsuranceService _healthInsuranceService;
    private readonly Mock<IHealthInsuranceRepository> _mockHealthInsuranceRepository;
    private readonly IFixture _fixture;
    
    #endregion
    
    public HealthInsuranceServiceUnitTest(ITestOutputHelper testOutputHelper)
    {
        _fixture = new Fixture();
        _testOutputHelper = testOutputHelper;
        
        _mockHealthInsuranceRepository = new Mock<IHealthInsuranceRepository>();
        _healthInsuranceService = new HealthInsuranceService(_mockHealthInsuranceRepository.Object);
    }

    #region GetHealthInsurances

    [Fact]
    public async Task GetHealthInsurances_ToBeEmpty()
    {
        // Arrange
        List<HealthInsurance> healthInsurances = new List<HealthInsurance>(); 
            
        // Act
        _mockHealthInsuranceRepository.Setup(temp => temp.GetAllAsync(It.IsAny<Expression<Func<HealthInsurance, bool>>>()))
            .ReturnsAsync(healthInsurances);
        List<HealthInsuranceSummaryResponse> summaryResponses = await _healthInsuranceService.GetHealthInsurances();
        
        // Assert
        summaryResponses.Should().BeEmpty();
    }

    [Fact]
    public async Task GetHealthInsurances_ValidList_ToBeSuccess()
    {
        List<HealthInsurance> healthInsurances = new List<HealthInsurance>()
        {
            _fixture.Build<HealthInsurance>().With(hi => hi.Employees, null as List<Employee>).With(hi => hi.Establishment, null as Establishment).Create(),
            _fixture.Build<HealthInsurance>().With(hi => hi.Employees, null as List<Employee>).With(hi => hi.Establishment, null as Establishment).Create(),
            _fixture.Build<HealthInsurance>().With(hi => hi.Employees, null as List<Employee>).With(hi => hi.Establishment, null as Establishment).Create()
        };

        List<HealthInsuranceSummaryResponse> expectedSummaryResponses =
            healthInsurances.Select(e => e.ToHealthInsuranceSummaryResponseObject()).ToList();
        _testOutputHelper.WriteLine("Expected:");
        foreach (HealthInsuranceSummaryResponse expectedSummaryResponse in expectedSummaryResponses)
        {
            _testOutputHelper.WriteLine(expectedSummaryResponse.ToString());
        }
        
        _mockHealthInsuranceRepository.Setup(temp => temp.GetAllAsync(It.IsAny<Expression<Func<HealthInsurance, bool>>>()))
            .ReturnsAsync(healthInsurances);

        List<HealthInsuranceSummaryResponse> actualSummaryResponses = await _healthInsuranceService.GetHealthInsurances();
        _testOutputHelper.WriteLine("Actual:");
        foreach (HealthInsuranceSummaryResponse actualSummaryResponse in actualSummaryResponses)
        {
            _testOutputHelper.WriteLine(actualSummaryResponse.ToString());
        }
        
        actualSummaryResponses.Should().BeEquivalentTo(expectedSummaryResponses);
    }

    #endregion

    #region AddHealthInsurance

    [Fact]
    public async Task AddHealthInsurance_NullAddRequest_ThrowsArgumentNullException()
    {
        HealthInsuranceAddRequest?  healthInsuranceAddRequest = null;

        Func<Task> action = async () => await _healthInsuranceService.AddHealthInsurance(healthInsuranceAddRequest);
        
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task AddHealthInsurance_ValidAddRequest_ToBeSuccess()
    {
        HealthInsuranceAddRequest healthInsuranceAddRequest = _fixture.Create<HealthInsuranceAddRequest>();

        HealthInsurance healthInsurance = healthInsuranceAddRequest.ToHealthInsuranceObject();

        HealthInsuranceSummaryResponse expectedSummaryResponse =
            healthInsurance.ToHealthInsuranceSummaryResponseObject();
 
        _mockHealthInsuranceRepository.Setup(temp => temp.AddAsync(It.IsAny<HealthInsurance>()))
            .ReturnsAsync(healthInsurance);

        HealthInsuranceSummaryResponse actualSummaryResponse =
            await _healthInsuranceService.AddHealthInsurance(healthInsuranceAddRequest);
        expectedSummaryResponse.Id = actualSummaryResponse.Id;

        actualSummaryResponse.Id.Should().NotBe(Guid.Empty);
        actualSummaryResponse.Should().Be(expectedSummaryResponse);
    }

    #endregion
    
    #region GetEstablishmentById

    [Fact]
    public async Task GetHealthInsuranceById_NullGetHealthInsuranceId_ThrowsArgumentNullException()
    {
        Guid? id =  null;

        Func<Task> action = async () => await _healthInsuranceService.GetHealthInsuranceById(id);
        
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task GetHealthInsuranceById_NullGetHealthInsuranceOrSoftDeleted_ThrowsKeyNotFoundException()
    {
        HealthInsurance healthInsurance = _fixture.Build<HealthInsurance>()
            .With(hi => hi.Employees, null as List<Employee>)
            .With(hi => hi.IsDeleted, true)
            .With(hi => hi.Establishment, null as Establishment)
            .Create();

        Func<Task> action = async () =>
        {
            await _healthInsuranceService.GetHealthInsuranceById(healthInsurance.Id);
        };
        
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task GetHealthInsuranceById_ValidGetHealthInsuranceId_ToBeSuccess()
    {
        HealthInsurance healthInsurance = _fixture.Build<HealthInsurance>()
            .With(hi => hi.Employees, null as List<Employee>)
            .With(hi => hi.IsDeleted, false)
            .With(hi => hi.Establishment, null as Establishment)
            .Create();

        HealthInsuranceDetailedResponse expectedResponse = healthInsurance.ToHealthInsuranceDetailedResponseObject();
        
        _mockHealthInsuranceRepository.Setup(temp => temp.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(healthInsurance);
        
        HealthInsuranceDetailedResponse? actualResponse = await _healthInsuranceService.GetHealthInsuranceById(healthInsurance.Id);
        
        actualResponse.Should().Be(expectedResponse);
    }

    #endregion

    #region UpdateHealthInsurance

    [Fact]
    public async Task UpdateHealthInsurance_NullUpdateRequest_ThrowsArgumentNullException()
    {
        HealthInsuranceUpdateRequest? healthInsuranceUpdateRequest =  null;

        Func<Task> action = async () =>
            await _healthInsuranceService.UpdateHealthInsurance(healthInsuranceUpdateRequest);
        
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdateHealthInsurance_NonExistId_ThrowsKeyNotFoundException()
    {
        HealthInsuranceUpdateRequest  healthInsuranceUpdateRequest = _fixture.Create<HealthInsuranceUpdateRequest>();
        
        Func<Task> action = async () => await _healthInsuranceService.UpdateHealthInsurance(healthInsuranceUpdateRequest);
        
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task UpdateHealthInsurance_ValidUpdateRequest_ToBeSuccess()
    {
        HealthInsurance healthInsurance = _fixture.Build<HealthInsurance>()
            .With(hi => hi.Employees, null as List<Employee>)
            .With(hi => hi.Establishment, null as Establishment)
            .Create();

        HealthInsuranceSummaryResponse expectedSummaryResponse =
            healthInsurance.ToHealthInsuranceSummaryResponseObject();

        HealthInsuranceUpdateRequest healthInsuranceUpdateRequest = expectedSummaryResponse.ToHealthInsuranceUpdateRequestObject();
        
        _mockHealthInsuranceRepository.Setup(temp => temp.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(healthInsurance);
        _mockHealthInsuranceRepository.Setup(temp => temp.UpdateAsync(It.IsAny<HealthInsurance>()))
            .ReturnsAsync(healthInsurance);

        HealthInsuranceSummaryResponse actualSummaryResponse =
            await _healthInsuranceService.UpdateHealthInsurance(healthInsuranceUpdateRequest);
        
        actualSummaryResponse.Should().Be(expectedSummaryResponse);
    }

    #endregion

    #region SoftDeleteHealthInsurance

    [Fact]
    public async Task SoftDeleteHealthInsurance_NullHealthInsurance_ThrowsKeyNotFoundException()
    {
        HealthInsurance healthInsurance = _fixture.Build<HealthInsurance>()
            .With(hi => hi.IsDeleted, false)
            .With(hi => hi.Employees, null as List<Employee>)
            .With(hi => hi.Establishment, null as Establishment)
            .Create();

        Func<Task> action = async () => await  _healthInsuranceService.SoftDeleteHealthInsurance(healthInsurance.Id);
        
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task SoftDeleteHealthInsurance_HealthInsuranceIsSoftDeleted_ThrowsInvalidOperationException()
    {
        HealthInsurance healthInsurance = _fixture.Build<HealthInsurance>()
            .With(hi => hi.IsDeleted, true)
            .With(hi => hi.Employees, null as List<Employee>)
            .With(hi => hi.Establishment, null as Establishment)
            .Create();
        
        _mockHealthInsuranceRepository.Setup(temp => temp.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(healthInsurance);

        Func<Task> action = async () => await _healthInsuranceService.SoftDeleteHealthInsurance(healthInsurance.Id);
        
        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    #endregion
}