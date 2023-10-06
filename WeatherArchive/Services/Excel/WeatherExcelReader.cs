using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using WeatherArchive.Extensions;
using WeatherArchive.Models;

namespace WeatherArchive.Services.Excel;

public class WeatherExcelReader: IExcelReader<WeatherPage>
{
    private IWorkbook workbook;
    public void Open(Stream stream)
    {
        workbook = new XSSFWorkbook(stream);
    }

    public IEnumerable<WeatherPage> ReadAll()
    {
        if (workbook is null)
            throw new InvalidOperationException();
        
        var result = new List<WeatherPage>();
        for(var i = 0; i < workbook.NumberOfSheets; i++)
        {
            var page = ReadPage(i);
            result.Add(page);
        }

        return result;
    }

    public WeatherPage ReadPage(int pageIndex)
    {
        if (workbook is null)
            throw new InvalidOperationException();
        
        var sheet = workbook.GetSheetAt(pageIndex);
        var page = new WeatherPage();
        for (var i = 4; i < sheet.LastRowNum; i++)
        {
            var row = ReadRow(sheet, i);
            page.Rows.Add(row);
        }

        return page;
    }

    private WeatherRow ReadRow(ISheet sheet, int rowIndex)
    {
        var row = sheet.GetRow(rowIndex);
        var date = row.GetStringSafety(0);
        var time = row.GetStringSafety(1);
        var temperature = row.GetNumericSafety(2);
        var humidity  = row.GetNumericSafety(3);
        var dewPoint = row.GetNumericSafety(4);
        var pressure = row.GetNumericSafety(5);
        var windDirection = row.GetStringSafety(6);
        var windSpeed = row.GetNumericSafety(7);
        var cloudy = row.GetNumericSafety(8);
        var cloudBase = row.GetNumericSafety(9);
        var visibility = row.GetNumericSafety(10);
        var weather = row.GetStringSafety(11);

        return new WeatherRow
        {
            Date = DateOnly.ParseExact(date, "dd.MM.yyyy"),
            Time = TimeSpan.Parse(time),
            Temperature = temperature,
            RelativeHumidity = humidity.ToNullableInt(),
            DewPoint = dewPoint,
            AtmospherePressure = pressure.ToNullableInt(),
            WindDirection = windDirection,
            WindSpeed = windSpeed.ToNullableInt(),
            Cloudy = cloudy.ToNullableInt(),
            CloudBase = cloudBase.ToNullableInt(),
            HorizontalVisibility = visibility.ToNullableInt(),
            WeatherConditions = weather
        };
    }
}