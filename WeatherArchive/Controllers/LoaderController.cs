using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherArchive.DataBase;
using WeatherArchive.Models;
using WeatherArchive.Services.Excel;
using WeatherArchive.ViewModels;

namespace WeatherArchive.Controllers;

public class LoaderController: Controller
{
    private IExcelReader<WeatherPage> _reader;
    private WeatherContext _context;

    public LoaderController(IExcelReader<WeatherPage> reader, WeatherContext context)
    {
        _reader = reader;
        _context = context;
    }


    public IActionResult Index(string? error)
    {
        return View(new LoaderViewModel{Error = error});
    }
    
    [HttpPost]
    public async Task<IActionResult> AddFile(IEnumerable<IFormFile> uploadedFiles)
    {
        if (uploadedFiles != null)
        {
            try
            {
                foreach (var uploadedFile in uploadedFiles)
                {
                    using (var stream = uploadedFile.OpenReadStream())
                    {
                        _reader.Open(stream);
                        var pages = _reader.ReadAll();
                        foreach (var page in pages)
                        {
                            foreach (var row in page.Rows)
                            {
                                _context.Add(row);
                            }
                        }

                        await _context.SaveChangesAsync();
                    }

                }     
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("Index", "Loader", 
                    new {error="При добавлении произошла коллизия записей. Убедитесь, что в файле и в архиве нет одинаковых записей."});
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Loader", new {error="Файл не распознан. Загрузите корректный файл."});
            }
        }
            
        return RedirectToAction("Index", "Archive");
    }
}