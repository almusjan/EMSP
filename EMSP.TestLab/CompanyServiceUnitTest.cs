using System.Linq.Expressions;
using AutoFixture;
using EMSP.Entities.Models;
using EMSP.RepositoryContracts.Interfaces;
using EMSP.ServiceContracts.DTOs.CompanyDTOs;
using EMSP.ServiceContracts.Extensions;
using EMSP.ServiceContracts.Interfaces;
using EMSP.Services;
using FluentAssertions;
using Moq;
using Xunit.Abstractions;

namespace EMSP.TestLab;

public class CompanyServiceUnitTest
{
    #region fields
    
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICompanyService _companyService;
    private readonly Mock<ICompanyRepository> _mockCompanyRepository;
    private readonly IFixture _fixture;
    
    #endregion
    
    public CompanyServiceUnitTest(ITestOutputHelper testOutputHelper)
    {
        _fixture = new Fixture();
        _testOutputHelper = testOutputHelper;
        
        _mockCompanyRepository = new Mock<ICompanyRepository>();
        _companyService = new CompanyService(_mockCompanyRepository.Object);
    }

    #region GetCompanies

    [Fact]
    public async Task GetCompanies_ToBeEmpty()
    {
        // Arrange
        List<Company> companies = new List<Company>(); 
            
        // Act
        _mockCompanyRepository.Setup(temp => temp.GetAllAsync(It.IsAny<Expression<Func<Company, bool>>>()))
            .ReturnsAsync(companies);
        List<CompanySummaryResponse> summaryResponses = await _companyService.GetCompanies();
        
        // Assert
        summaryResponses.Should().BeEmpty();
    }

    [Fact]
    public async Task GetCompanies_ValidList_ToBeSuccess()
    {
        List<Company> companies = new List<Company>()
        {
            _fixture.Build<Company>().With(c => c.Establishment, null as Establishment).With(c => c.Employees,null as List<Employee>).Create(),
            _fixture.Build<Company>().With(c => c.Establishment, null as Establishment).With(c => c.Employees,null as List<Employee>).Create(),
            _fixture.Build<Company>().With(c => c.Establishment, null as Establishment).With(c => c.Employees,null as List<Employee>).Create()
        };

        List<CompanySummaryResponse> expectedCompanySummaryResponses =
            companies.Select(c => c.ToCompanySummaryResponseObject()).ToList();
        _testOutputHelper.WriteLine("Expected:");
        foreach (CompanySummaryResponse expectedSummaryResponse in expectedCompanySummaryResponses)
        {
            _testOutputHelper.WriteLine(expectedSummaryResponse.ToString());
        }
        
        _mockCompanyRepository.Setup(temp => temp.GetAllAsync(It.IsAny<Expression<Func<Company, bool>>>()))
            .ReturnsAsync(companies);
        
        List<CompanySummaryResponse> actualCompanySummaryResponses = await _companyService.GetCompanies();
        _testOutputHelper.WriteLine("Actual:");
        foreach (CompanySummaryResponse actualSummaryResponse in actualCompanySummaryResponses)
        {
            _testOutputHelper.WriteLine(actualSummaryResponse.ToString());
        }
        
        actualCompanySummaryResponses.Should().BeEquivalentTo(expectedCompanySummaryResponses);
    }

    #endregion

    #region AddCompany

    [Fact]
    public async Task AddCompany_NullAddRequest_ThrowsArgumentNullException()
    {
        CompanyAddRequest?  companyAddRequest = null;
        
        Func<Task> action = async () => await _companyService.AddCompany(companyAddRequest);
        
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task AddCompany_ValidAddRequest_ToBeSuccess()
    {
        CompanyAddRequest companyAddRequest = _fixture.Create<CompanyAddRequest>();

        Company company = companyAddRequest.ToCompanyObject();
        
        CompanySummaryResponse expectedSummaryResponse = company.ToCompanySummaryResponseObject();
 
        _mockCompanyRepository.Setup(temp => temp.AddAsync(It.IsAny<Company>()))
            .ReturnsAsync(company);
        
        CompanySummaryResponse actualSummaryResponse = await _companyService.AddCompany(companyAddRequest);
        expectedSummaryResponse.Id = actualSummaryResponse.Id;

        actualSummaryResponse.Id.Should().NotBe(Guid.Empty);
        actualSummaryResponse.Should().Be(expectedSummaryResponse);
    }

    #endregion
    
    #region GetCompanyById

    [Fact]
    public async Task GetCompanyById_NullCompanyId_ThrowsArgumentNullException()
    {
        Guid? id =  null;
        
        Func<Task> action = async () => await _companyService.GetCompanyById(id);
        
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task GetCompanyById_NullCompanyOrSoftDeleted_ThrowsKeyNotFoundException()
    {
        Company company = _fixture.Build<Company>()
            .With(c => c.IsDeleted, true)
            .With(c => c.Establishment, null as Establishment)
            .With(c => c.Employees, null as List<Employee>).Create();

        Func<Task> action = async () =>
        {
            await _companyService.GetCompanyById(company.Id);
        };
        
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task GetCompanyById_ValidCompanyId_ToBeSuccess()
    {
        Company company = _fixture.Build<Company>()
            .With(c => c.IsDeleted, false)
            .With(c => c.Establishment, null as Establishment)
            .With(c => c.Employees, null as List<Employee>).Create();
        
        CompanyDetailedResponse expectedResponse = company.ToCompanyDetailedResponseObject();
        
        _mockCompanyRepository.Setup(temp => temp.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(company);
        
        CompanyDetailedResponse? actualResponse = await _companyService.GetCompanyById(company.Id);
        
        actualResponse.Should().Be(expectedResponse);
    }

    #endregion

    #region UpdateCompany

    [Fact]
    public async Task UpdateCompany_NullUpdateRequest_ThrowsArgumentNullException()
    {
        CompanyUpdateRequest? companyUpdateRequest =  null;
        
        Func<Task> action = async () => await _companyService.UpdateCompany(companyUpdateRequest);
        
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdateCompany_NonExistId_ThrowsKeyNotFoundException()
    {
        CompanyUpdateRequest  companyUpdateRequest = _fixture.Create<CompanyUpdateRequest>();
        
        Func<Task> action = async () => await _companyService.UpdateCompany(companyUpdateRequest);
        
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task UpdateCompany_ValidUpdateRequest_ToBeSuccess()
    {
        Company company = _fixture.Build<Company>()
            .With(c => c.IsDeleted, false)
            .With(c => c.Establishment, null as Establishment)
            .With(c => c.Employees, null as List<Employee>).Create();
        
        CompanySummaryResponse expectedCompanySummaryResponse =  company.ToCompanySummaryResponseObject();

        CompanyUpdateRequest companyUpdateRequest = expectedCompanySummaryResponse.ToCompanyUpdateRequestObject();
        
        _mockCompanyRepository.Setup(temp => temp.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(company);
        _mockCompanyRepository.Setup(temp => temp.UpdateAsync(It.IsAny<Company>()))
            .ReturnsAsync(company);
        
        CompanySummaryResponse actualCompanySummaryResponse = await _companyService.UpdateCompany(companyUpdateRequest);
        
        actualCompanySummaryResponse.Should().Be(expectedCompanySummaryResponse);
    }

    #endregion

    #region SoftDeleteCompany

    [Fact]
    public async Task SoftDeleteCompany_NullCompany_ThrowsKeyNotFoundException()
    {
        Company company = _fixture.Build<Company>()
            .With(c => c.IsDeleted, false)
            .With(c => c.Establishment, null as Establishment)
            .With(c => c.Employees, null as List<Employee>).Create();
        
        Func<Task> action = async () => await _companyService.SoftDeleteCompany(company.Id);
        
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task SoftDeleteCompany_CompanyIsSoftDeleted_ThrowsInvalidOperationException()
    {
        Company company = _fixture.Build<Company>()
            .With(c => c.IsDeleted, true)
            .With(c => c.Establishment, null as Establishment)
            .With(c => c.Employees, null as List<Employee>).Create();
        
        _mockCompanyRepository.Setup(temp => temp.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(company);
        
        Func<Task> action = async () => await _companyService.SoftDeleteCompany(company.Id);
        
        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    #endregion
}