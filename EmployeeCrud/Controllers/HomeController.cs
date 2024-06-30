using EmployeeCrud.ApplicationDbContext;
using EmployeeCrud.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace EmployeeCrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmployeeDbContext _employeeDbContext;
       

        public HomeController(ILogger<HomeController> logger, EmployeeDbContext employeeDbContext)
        {
            _logger = logger;
            _employeeDbContext = employeeDbContext;   
        }

        public IActionResult Index()
        {
            return View();
        }
     
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Uploadfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Uploadfile(IFormFile file) 
        {

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (file != null && file.Length > 0)
                {
                var uploadfolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Upload\\";
                if(!Directory.Exists(uploadfolder))
                {
                    Directory.CreateDirectory(uploadfolder);
                }
                var filepath = Path.Combine(uploadfolder, file.FileName);

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                using (var stream = System.IO.File.Open(filepath, FileMode.Open, FileAccess.Read))
                {
                    
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    { 
                        do
                        {
                            bool isHeaderSkipped = false;
                            while (reader.Read())
                            {

                                if (!isHeaderSkipped)
                                {
                                    isHeaderSkipped = true;
                                    continue;
                                }

                                StudentModel s = new StudentModel();
                                s.Name = reader.GetValue(1).ToString();
                                s.Marks = Convert.ToInt32(reader.GetValue(2).ToString());

                                _employeeDbContext.Add(s);
                                await _employeeDbContext.SaveChangesAsync();    
                            }
                        } while (reader.NextResult());

                        ViewBag.Message = "success";
                    }
                }
            }

            else
                ViewBag.Message = "empty";
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
