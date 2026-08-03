using System.Linq.Expressions;
using AutoFixture;
using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using EMSP.ServiceContracts.DTOs.EstablishmentDTOs;
using EMSP.ServiceContracts.Extensions;
using EMSP.ServiceContracts.Interfaces;
using EMSP.Services;
using FluentAssertions;
using Moq;
using Xunit.Abstractions;

namespace EMSP.TestLab;

public class EstablishmentServiceUnitTest
{
    #region fields
    
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly IEstablishmentService _establishmentService;
    private readonly Mock<IEstablishmentRepository> _mockEstablishmentRepository;
    private readonly IFixture _fixture;
    
    #endregion
    
    public EstablishmentServiceUnitTest(ITestOutputHelper testOutputHelper)
    {
        _fixture = new Fixture();
        _testOutputHelper = testOutputHelper;
        
        _mockEstablishmentRepository = new Mock<IEstablishmentRepository>();
        _establishmentService = new EstablishmentService(_mockEstablishmentRepository.Object);
    }

    #region GetEstablishments

    [Fact]
    public async Task GetEstablishments_ToBeEmpty()
    {
        // Arrange
        List<Establishment> establishments = new List<Establishment>(); 
            
        // Act
        _mockEstablishmentRepository.Setup(temp => temp.GetAllAsync(It.IsAny<Expression<Func<Establishment, bool>>>()))
            .ReturnsAsync(establishments);
        List<EstablishmentSummaryResponse> summaryResponses = await _establishmentService.GetEstablishments();
        
        // Assert
        summaryResponses.Should().BeEmpty();
    }

    [Fact]
    public async Task GetEstablishments_ValidList_ToBeSuccess()
    {
        List<Establishment> establishments = new List<Establishment>()
        {
            _fixture.Build<Establishment>().With(e => e.Companies, null as List<Company>).With(e => e.HealthInsurances, null as List<HealthInsurance>).With(e => e.Employees,null as List<Employee>).Create(),
            _fixture.Build<Establishment>().With(e => e.Companies, null as List<Company>).With(e => e.HealthInsurances, null as List<HealthInsurance>).With(e => e.Employees,null as List<Employee>).Create(),
            _fixture.Build<Establishment>().With(e => e.Companies, null as List<Company>).With(e => e.HealthInsurances, null as List<HealthInsurance>).With(e => e.Employees,null as List<Employee>).Create()
        };

        List<EstablishmentSummaryResponse> expectedSummaryResponses =
            establishments.Select(e => e.ToEstablishmentSummaryResponseObject()).ToList();
        _testOutputHelper.WriteLine("Expected:");
        foreach (EstablishmentSummaryResponse expectedSummaryResponse in expectedSummaryResponses)
        {
            _testOutputHelper.WriteLine(expectedSummaryResponse.ToString());
        }
        
        _mockEstablishmentRepository.Setup(temp => temp.GetAllAsync(It.IsAny<Expression<Func<Establishment, bool>>>()))
            .ReturnsAsync(establishments);
        
        List<EstablishmentSummaryResponse> actualSummaryResponses = await _establishmentService.GetEstablishments();
        _testOutputHelper.WriteLine("Actual:");
        foreach (EstablishmentSummaryResponse actualSummaryResponse in actualSummaryResponses)
        {
            _testOutputHelper.WriteLine(actualSummaryResponse.ToString());
        }
        
        actualSummaryResponses.Should().BeEquivalentTo(expectedSummaryResponses);
    }

    #endregion

    #region AddEstablishment

    [Fact]
    public async Task AddEstablishment_NullAddRequest_ThrowsArgumentNullException()
    {
        EstablishmentAddRequest?  establishmentAddRequest = null;

        Func<Task> action = async () => await _establishmentService.AddEstablishment(establishmentAddRequest);
        
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task AddEstablishment_ValidAddRequest_ToBeSuccess()
    {
        EstablishmentAddRequest establishmentAddRequest = _fixture.Create<EstablishmentAddRequest>();

        Establishment establishment = establishmentAddRequest.ToEstablishmentObject();
        
        EstablishmentSummaryResponse expectedSummaryResponse = establishment.ToEstablishmentSummaryResponseObject();
 
        _mockEstablishmentRepository.Setup(temp => temp.AddAsync(It.IsAny<Establishment>()))
            .ReturnsAsync(establishment);

        EstablishmentSummaryResponse actualSummaryResponse =
            await _establishmentService.AddEstablishment(establishmentAddRequest);
        expectedSummaryResponse.Id = actualSummaryResponse.Id;

        actualSummaryResponse.Id.Should().NotBe(Guid.Empty);
        actualSummaryResponse.Should().Be(expectedSummaryResponse);
    }

    #endregion
    
    #region GetEstablishmentById

    [Fact]
    public async Task GetEstablishmentById_NullEstablishmentId_ThrowsArgumentNullException()
    {
        Guid? id =  null;

        Func<Task> action = async () => await _establishmentService.GetEstablishmentById(id);
        
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task GetEstablishmentById_NullEstablishmentOrSoftDeleted_ThrowsKeyNotFoundException()
    {
        Establishment establishment = _fixture.Build<Establishment>()
            .With(e => e.IsDeleted, true)
            .With(e => e.Companies, null as List<Company>)
            .With(e => e.HealthInsurances, null as List<HealthInsurance>)
            .With(e => e.Employees, null as List<Employee>).Create();

        Func<Task> action = async () =>
        {
            await _establishmentService.GetEstablishmentById(establishment.Id);
        };
        
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task GetEstablishmentById_ValidEstablishmentId_ToBeSuccess()
    {
        Establishment establishment = _fixture.Build<Establishment>()
            .With(e => e.IsDeleted, false)
            .With(e => e.Companies, null as List<Company>)
            .With(e => e.HealthInsurances, null as List<HealthInsurance>)
            .With(e => e.Employees, null as List<Employee>).Create();

        EstablishmentDetailedResponse expectedResponse = establishment.ToEstablishmentDetailedResponseObject();
        
        _mockEstablishmentRepository.Setup(temp => temp.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(establishment);
        
        EstablishmentDetailedResponse? actualResponse = await _establishmentService.GetEstablishmentById(establishment.Id);
        
        actualResponse.Should().Be(expectedResponse);
    }

    #endregion

    #region UpdateEstablishment

    [Fact]
    public async Task UpdateEstablishment_NullUpdateRequest_ThrowsArgumentNullException()
    {
        EstablishmentUpdateRequest? establishmentUpdateRequest =  null;

        Func<Task> action = async () => await _establishmentService.UpdateEstablishment(establishmentUpdateRequest);
        
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdateEstablishment_NonExistId_ThrowsKeyNotFoundException()
    {
        EstablishmentUpdateRequest  establishmentUpdateRequest = _fixture.Create<EstablishmentUpdateRequest>();
        
        Func<Task> action = async () => await _establishmentService.UpdateEstablishment(establishmentUpdateRequest);
        
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task UpdateEstablishment_ValidUpdateRequest_ToBeSuccess()
    {
        Establishment establishment = _fixture.Build<Establishment>()
            .With(e => e.IsDeleted, true)
            .With(e => e.Companies, null as List<Company>)
            .With(e => e.HealthInsurances, null as List<HealthInsurance>)
            .With(e => e.Employees, null as List<Employee>).Create();

        EstablishmentSummaryResponse expectedSummaryResponse = establishment.ToEstablishmentSummaryResponseObject();

        EstablishmentUpdateRequest establishmentUpdateRequest = expectedSummaryResponse.ToEstablishmentUpdateRequest();
        
        _mockEstablishmentRepository.Setup(temp => temp.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(establishment);
        _mockEstablishmentRepository.Setup(temp => temp.UpdateAsync(It.IsAny<Establishment>()))
            .ReturnsAsync(establishment);

        EstablishmentSummaryResponse actualSummaryResponse =
            await _establishmentService.UpdateEstablishment(establishmentUpdateRequest);
        
        actualSummaryResponse.Should().Be(expectedSummaryResponse);
    }

    #endregion

    #region SoftDeleteEstablishment

    [Fact]
    public async Task SoftDeleteEstablishment_NullEstablishment_ThrowsKeyNotFoundException()
    {
        Establishment establishment = _fixture.Build<Establishment>()
            .With(e => e.IsDeleted, false)
            .With(e => e.Companies, null as List<Company>)
            .With(e => e.HealthInsurances, null as List<HealthInsurance>)
            .With(e => e.Employees, null as List<Employee>).Create();

        Func<Task> action = async () => await _establishmentService.SoftDeleteEstablishment(establishment.Id);
        
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task SoftDeleteEstablishment_EstablishmentIsSoftDeleted_ThrowsInvalidOperationException()
    {
        Establishment establishment = _fixture.Build<Establishment>()
            .With(e => e.IsDeleted, true)
            .With(e => e.Companies, null as List<Company>)
            .With(e => e.HealthInsurances, null as List<HealthInsurance>)
            .With(e => e.Employees, null as List<Employee>).Create();
        
        _mockEstablishmentRepository.Setup(temp => temp.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(establishment);
        
        Func<Task> action = async () => await _establishmentService.SoftDeleteEstablishment(establishment.Id);
        
        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    #endregion
}