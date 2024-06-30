using EmployeeCrud.Models;
using EmployeeCrud.ApplicationDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Hosting;

namespace EmployeeCrud.Controllers
{
    public class AjaxController : Controller
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public AjaxController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            List<EmployeeModel> employeeModel = new List<EmployeeModel>();
            employeeModel = _employeeDbContext.Employees.ToList();
            return Json(employeeModel);
        }
        [HttpPost]
        public IActionResult Add([FromBody] EmployeeModel employeeModel)
        {

            if (ModelState.IsValid)
            {
                _employeeDbContext.Employees.Add(employeeModel);
                _employeeDbContext.SaveChanges();
                return Json("Data Saved ");
            }
            else
            {
                return Json("Data not Saved ");
            }


        }
        [HttpGet]
        public IActionResult GetById(int id)
        {

            var Data = _employeeDbContext.Employees.FirstOrDefault(u => u.ID == id);
            return Json(Data);

        }
       [HttpPost]
        public IActionResult Edit([FromBody] EmployeeModel employeeModel)
        {
            var employee = _employeeDbContext.Employees.Find(employeeModel.ID);
            if (employee == null)
            {
                return NotFound("Post not found.");
            }
            if (ModelState.IsValid)
            {

                employee.Name = employeeModel.Name;
                employee.Address = employeeModel.Address;
                employee.Date = employeeModel.Date;
                employee.Status = employeeModel.Status;
                _employeeDbContext.Employees.Update(employee);
                _employeeDbContext.SaveChanges();

                return Ok(employee);
            }
           else

            {
                return BadRequest();
            }

        }

        //[HttpPost]
        //public IActionResult DeleteById(int id)
        //{
        //    var employee = _employeeDbContext.Employees.FirstOrDefault(u => u.ID == id);
        //    if (employee == null)
        //    {
        //        return NotFound();

        //    }
        //    else
        //    {
        //        _employeeDbContext.Remove(employee);
        //        _employeeDbContext.SaveChanges();
        //        return Ok("Delete successfully ");
        //    }
        //}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var employee = _employeeDbContext.Employees.FirstOrDefault(u => u.ID==id);
            if(employee == null)
            {
                return NotFound();

            }
            else
            {
                _employeeDbContext.Remove(employee);
                _employeeDbContext.SaveChanges();
                return Ok("Delete successfully ");
            }
        }

   
        //public IActionResult MyAccountIndex()
        //{
        //    List<MyAccountModel> myAccountModels = new List<MyAccountModel>();
        //    myAccountModels = _employeeDbContext.MyAccount.ToList();
        //    return Json(myAccountModels);

        //}

        [HttpPost]
        public IActionResult Register([FromBody] MyAccountModel myAccountModel)
        {
            if(ModelState.IsValid)
            {
                _employeeDbContext.MyAccount.Add(myAccountModel);
                _employeeDbContext.SaveChanges();
                return RedirectToAction("MyAccountIndex");
            }
            else
            {
                return BadRequest();
            }
        }
       
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn([FromBody] MyAccountModel myAccountModel)
        {
            var singinData = _employeeDbContext.MyAccount.Where(x => x.Email == myAccountModel.Email).FirstOrDefault();
            if(singinData != null)
            {
               
                if(singinData.Email== myAccountModel.Email && singinData.Password== myAccountModel.Password)
                {
                    HttpContext.Session.SetString("Id", singinData.Id.ToString());
                    HttpContext.Session.SetString("Name", singinData.Name);
                    return RedirectToAction("MyAccountIndex");
                }
            }
            else
            {
                return BadRequest();
            }
            return View();
        }
    }
}
