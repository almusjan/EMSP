using EMSP.Entities;
using EMSP.ServiceContracts.Interfaces;
using EMSP.Services;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add My Services - start
builder.Services.AddScoped<IBankService, BankService>();
builder.Services.AddScoped<ICompanyService,  CompanyService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeCostService, EmployeeCostService>();
builder.Services.AddScoped<IEstablishmentService, EstablishmentService>();
builder.Services.AddScoped<IHealthInsuranceService,  HealthInsuranceService>();
builder.Services.AddScoped<ISalaryService, SalaryService>();
// Add My Services - end

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseSnakeCaseNamingConvention();
});

builder.Services.AddHttpLogging(loggingBuilder =>
{
    loggingBuilder.LoggingFields = HttpLoggingFields.RequestProperties | HttpLoggingFields.ResponseHeaders;
});
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpLogging();
app.UseStaticFiles();
app.MapControllers();

// auto insert banks and countries from first launch!
// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//     DbSeeder.Seed(dbContext);
// }

app.Run();