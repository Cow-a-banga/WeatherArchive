using WeatherArchive.Models;

namespace WeatherArchive.ViewModels;

public class ArchiveViewModel
{
    public FilterViewModel Filter { get; set; }
    public PaginationModel Pagination { get; set; }
    public List<WeatherRow> Items { get; set; }
    
}