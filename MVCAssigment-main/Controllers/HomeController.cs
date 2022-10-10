using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCAssigment.Models;
using System.Text.Json;
using CsvHelper;
namespace MVCAssigment.Controllers;
[Route("Nashtech/Rookies")]
public class HomeController : Controller
{
    private static List<Person> _list = new List<Person>{        
            new Person
            {
                FirstName = "Phan",
                LastName = "Nam",
                Gender = "Male",
                DateOfBirth = new DateTime(1999, 10, 18),
                PhoneNumber = "0396373132",
                BirthPlace = "Ha Noi",
                Age = 21,
                IsGraduated = true
            },
            new Person
            {
                FirstName = "Tran",
                LastName = "Linh",
                Gender = "FeMale",
                DateOfBirth = new DateTime(2003, 10, 15),
                PhoneNumber = "0396373132",
                BirthPlace = "Bac Ninh",
                Age = 29,
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Dao",
                LastName = "Trang",
                Gender = "FeMale",
                DateOfBirth = new DateTime(2003, 07, 13),
                PhoneNumber = "0396373132",
                BirthPlace = "SG",
                Age = 29,
                IsGraduated = true
            },
            new Person
            {
                FirstName = "Vu",
                LastName = "Kim",
                Gender = "Male",
                DateOfBirth = new DateTime(2003, 11, 30),
                PhoneNumber = "0396373132",
                BirthPlace = "Ha Noi",
                Age = 29,
                IsGraduated = true
            },
            new Person
            {
                FirstName = "Duy",
                LastName = "Anh",
                Gender = "Male",
                DateOfBirth = new DateTime(2000, 11, 30),
                PhoneNumber = "0396373132",
                BirthPlace = "Ha Noi",
                Age = 21,
                IsGraduated = true
            }
    };
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [Route("GetMalePerson")]
    public IActionResult GetMalePerson()
    {
        var data = _list.Where(p => p.Gender == "Male");
        return Json(data);
    }
    [Route("GetOldestPerson")]
    public IActionResult GetOldestPerson()
    {
        var maxAge = _list.Max(p => p.Age);
        var oldest = _list.FirstOrDefault(p => p.Age == maxAge);
        return Json(oldest);
    }
    [Route("GetFullNames")]
    public IActionResult GetFullNames()
    {
        var fullNames = _list.Select(p => p.LastName + " " + p.FirstName);
        return Json(fullNames);
    }
    [Route("index")]
    public ContentResult Index()
    {
        var data = JsonSerializer.Serialize(_list);
        return Content(data);
    }

    #region Export
    public byte[] WriteCsvToMemory(IEnumerable<Person> person)
    {
        using (var memoryStream = new MemoryStream())
        using (var streamWriter = new StreamWriter(memoryStream))
        using (var csvWriter = new CsvWriter(streamWriter, System.Globalization.CultureInfo.CurrentCulture))
        {
            csvWriter.WriteRecords(person);
            streamWriter.Flush();
            return memoryStream.ToArray();
        }
    }

    public FileStreamResult ExportPayments()
    {
        var result = WriteCsvToMemory(_list);
        var memoryStream = new MemoryStream(result);
        return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = "person.csv" };
    }
    #endregion
    #region 
    public IActionResult GetPersonByBirthYear(int year, string compareType)
    {
        switch (compareType)
        {
            case "equals":
                return Json(_list.Where(p => p.DateOfBirth.Year == year));
            case "greaterThan":
                return Json(_list.Where(p => p.DateOfBirth.Year > year));
            case "lessThan":
                return Json(_list.Where(p => p.DateOfBirth.Year < year));
            default:
                return Json(null);
        }
    }

    public IActionResult GetPersonWhoBornIn2000()
    {
        return RedirectToAction("GetPersonByBirthYear", new { year = 2000, compareType = "equals" });
    }

    public IActionResult GetPersonWhoBornAfter2000()
    {
        return RedirectToAction("GetPersonByBirthYear", new { year = 2000, compareType = "greaterThan" });
    }

    public IActionResult GetPersonWhoBornBefore2000()
    {
        return RedirectToAction("GetPersonByBirthYear", new { year = 2000, compareType = "lessThan" });
    }
    #endregion
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
