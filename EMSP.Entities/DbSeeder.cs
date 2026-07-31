using EMSP.Entities.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace EMSP.Entities;

public static class DbSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        context.Database.Migrate();

        if (!context.Banks.Any())
        {
            var banks = ReadBanksFromExcel("banks.xlsx");
            context.Banks.AddRange(banks);
            context.SaveChanges();
        }
        
        if (!context.Countries.Any())
        {
            var countries = ReadCountriesFromExcel("nationalities.xlsx");
            context.Countries.AddRange(countries);
            context.SaveChanges();
        }
    }
    
    private static List<Bank> ReadBanksFromExcel(string filePath)
    {
        List<Bank> banks = new List<Bank>();
        
        if(!File.Exists(filePath))
            throw new FileNotFoundException($"File was not found: {filePath}");
        
        using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath)))
        {
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Banks"];
            var rowCount = worksheet.Dimension.Rows;
            
            if (worksheet == null || worksheet.Dimension == null)
                throw new Exception("الورقة 'Banks' غير موجودة أو فارغة.");

            for (int row = 2; row <= rowCount; row++)
            {
                Bank bank = new Bank()
                {
                    Id = Guid.NewGuid(),
                    BankNameAr = worksheet.Cells[row, 1].Text,
                    BankNameEn = worksheet.Cells[row, 2].Text,
                    CreatedAt = DateTime.UtcNow
                };
                banks.Add(bank);
            }
        }
        return banks;
    }

    private static List<Country> ReadCountriesFromExcel(string filePath)
    {
        List<Country> countries = new List<Country>();
        
        if(!File.Exists(filePath))
            throw new FileNotFoundException($"File was not found: {filePath}");
        
        using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath)))
        {
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Countries"];
            var rowCount = worksheet.Dimension.Rows;
            
            if (worksheet == null || worksheet.Dimension == null)
                throw new Exception("الورقة 'Countries' غير موجودة أو فارغة.");

            for (int row = 2; row <= rowCount; row++)
            {
                Country country = new Country()
                {
                    Id =  Guid.NewGuid(),
                    CountryNameEn =  worksheet.Cells[row, 1].Text,
                    NationalityEn =   worksheet.Cells[row, 2].Text,
                    CountryNameAr =  worksheet.Cells[row, 3].Text,
                    NationalityAr =   worksheet.Cells[row, 4].Text,
                    CountryCode =  worksheet.Cells[row, 5].Text,
                    CreatedAt =  DateTime.UtcNow
                };
                countries.Add(country);
            }
        }
        return countries;
    }
}