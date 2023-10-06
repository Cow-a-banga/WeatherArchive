namespace WeatherArchive.Services.Excel;

public interface IExcelReader<T>
{
    public void Open(Stream stream);
    public IEnumerable<T> ReadAll();
    public T ReadPage(int pageIndex);
}