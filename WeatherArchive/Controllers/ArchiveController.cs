using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WeatherArchive.DataBase;
using WeatherArchive.Models;
using WeatherArchive.ViewModels;

namespace WeatherArchive.Controllers;

public class ArchiveController: Controller
{
    private WeatherContext _context;

    public ArchiveController(WeatherContext context)
    {
        _context = context;
    }

    private List<SelectListItem> GetYearsFilter(int? year)
    {
        var years = _context.WeatherRows
            .Select(x => x.Date.Year as int?)
            .Distinct()
            .OrderBy(x => x)
            .ToList()
            .Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() })
            .ToList();
        years.Insert(0, new SelectListItem{Text = null, Value = null});
        if (year.HasValue)
        {
            var yearStr = year.ToString();
            var selected = years.FirstOrDefault(x => x.Value == yearStr);
            if(selected != null)
                selected.Selected = true;
        }

        return years;
    }
    
    private List<SelectListItem> GetMonthsFilter(int? month)
    {
        var months = new List<SelectListItem>
        {
            new() { Text = null, Value = null },
            new() { Text = "Январь", Value = "1" },
            new() { Text = "Февраль", Value = "2" },
            new() { Text = "Март", Value = "3" },
            new() { Text = "Апрель", Value = "4" },
            new() { Text = "Май", Value = "5" },
            new() { Text = "Июнь", Value = "6" },
            new() { Text = "Июль", Value = "7" },
            new() { Text = "Август", Value = "8" },
            new() { Text = "Сентябрь", Value = "9" },
            new() { Text = "Октябрь", Value = "10" },
            new() { Text = "Ноябрь", Value = "11" },
            new() { Text = "Декабрь", Value = "12" },
        };

        if (month.HasValue)
        {
            var monthStr = month.ToString();
            var selected = months.FirstOrDefault(x => x.Value == monthStr);
            if(selected != null)
                selected.Selected = true;
        }

        return months;
    }

    public IActionResult Index(int page = 1, int? year = null, int? month = null)
    {
        const int itemsOnPage = 20;

        if (page < 1)
            page = 1;

        var rowsQuery = _context.WeatherRows.AsQueryable();

        if (year.HasValue)
        {
            rowsQuery = rowsQuery.Where(x => x.Date.Year == year);
        }
        
        if (month.HasValue)
        {
            rowsQuery = rowsQuery.Where(x => x.Date.Month == month);
        }
        
        double itemsNumber = rowsQuery.Count();

        var rows = rowsQuery
            .OrderBy(x => x.Date)
            .ThenBy(x => x.Time)
            .Skip((page - 1) * itemsOnPage)
            .Take(itemsOnPage)
            .ToList();
        
        var model = new ArchiveViewModel
        {
            Items = rows,
            Filter = new FilterViewModel
            {
                Years = GetYearsFilter(year), 
                Year = year,
                Months = GetMonthsFilter(month),
                Month = month,
            },
            Pagination = new PaginationModel
            {
                CurrentPage = page,
                PagesNumber = (int)Math.Ceiling(itemsNumber / itemsOnPage),
            }
        };
        return View(model);
    }
}