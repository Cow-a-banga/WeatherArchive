using WeatherArchive.Models;

namespace WeatherArchive.ViewModels;

public class PaginationViewModel
{
    public PaginationModel Pagination { get; set; }
    public FilterViewModel Filter { get; set; }
}