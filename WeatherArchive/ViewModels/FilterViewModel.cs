using Microsoft.AspNetCore.Mvc.Rendering;

namespace WeatherArchive.ViewModels;

public class FilterViewModel
{
    public List<SelectListItem> Years { get; set; }
    public int? Year { get; set; }
    
    public List<SelectListItem> Months { get; set; }
    public int? Month { get; set; }
    
}