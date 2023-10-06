using NPOI.SS.UserModel;

namespace WeatherArchive.Extensions;

public static class RowExtensions
{
    public static string? GetStringSafety(this IRow row, int cellIndex)
    {
        var cell = row.GetCell(cellIndex);
        if (cell == null || cell.CellType != CellType.String)
            return null;
        return cell.StringCellValue;
    }
    
    public static double? GetNumericSafety(this IRow row, int cellIndex)
    {
        var cell = row.GetCell(cellIndex);
        if (cell == null || cell.CellType != CellType.Numeric)
            return null;
        return cell.NumericCellValue;
    }
}