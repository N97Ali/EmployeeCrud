using EmployeeCrud.ApplicationDbContext;
using EmployeeCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EmployeeCrud.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }
        public async Task<IActionResult> Index(string sortOrder , string searchString)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;

            var employees = from e in _employeeDbContext.Employees
                            select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e => e.Name.Contains(searchString)); 
            }
            switch (sortOrder)
            {
                case "name_asc":
                    employees = employees.OrderBy(e => e.Name);
                    break;
                case "name_desc":
                    employees = employees.OrderByDescending(e => e.Name);
                    break;
                default:
                    employees = employees.OrderBy(e => e.Name);
                    break;
            }

            return View(await employees.AsNoTracking().ToListAsync());
        }
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee(EmployeeModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                _employeeDbContext.Add(employeeModel);
                _employeeDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Employee = _employeeDbContext.Employees.Find(id);
            return View(Employee);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeModel employeeModel)
        {
            var Employee = _employeeDbContext.Employees.Find(employeeModel.ID);
            if (Employee is not null)
            {
                Employee.Name = employeeModel.Name;
                Employee.Address = employeeModel.Address;
                Employee.Date = employeeModel.Date;
                Employee.Status = employeeModel.Status;

                _employeeDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var Employee = _employeeDbContext.Employees.Find(id);
            return View(Employee);
        }
        [HttpPost]
        public IActionResult Delete(EmployeeModel employeeModel)
        {
            var Employee = _employeeDbContext.Employees.Find(employeeModel.ID);
            if (Employee is not null)
            {
                _employeeDbContext.Remove(Employee);
                _employeeDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        public IActionResult MyAccountIndex()
        {
            return View(_employeeDbContext.MyAccount.ToList());
        }
        [HttpGet]
        public IActionResult MyAccountAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MyAccountAdd(MyAccountModel myAccountModel)
        {
            if (ModelState.IsValid)
            {
                _employeeDbContext.Add(myAccountModel);
                _employeeDbContext.SaveChanges();
                return RedirectToAction("MyAccountIndex");
            }
            return View();
        }
        [HttpGet]
        public IActionResult MyAccountEdit(int id)
        {
            var Employee = _employeeDbContext.MyAccount.Find(id);
            return View(Employee);
        }
        [HttpPost]
        public IActionResult MyAccountEdit(MyAccountModel myAccountModel)
        {
            var MyAccount = _employeeDbContext.MyAccount.Find(myAccountModel.Id);
            if (MyAccount is not null)
            {
                MyAccount.Name = myAccountModel.Name;
                MyAccount.Email = myAccountModel.Email;
                MyAccount.Password = myAccountModel.Password;
                MyAccount.Phone = myAccountModel.Phone;

                _employeeDbContext.SaveChanges();
                return RedirectToAction("MyAccountIndex");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult MyAccountDelete(int id)
        {
            var Employee = _employeeDbContext.MyAccount.Find(id);
            return View(Employee);
        }
        [HttpPost]
        public IActionResult MyAccountDelete(MyAccountModel myAccountModel)
        {
            var MyAccount = _employeeDbContext.MyAccount.Find(myAccountModel.Id);
            if (MyAccount is not null)
            {
                _employeeDbContext.Remove(MyAccount);
                _employeeDbContext.SaveChanges();
                return RedirectToAction("MyAccountIndex");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Login(MyAccountModel myAccount)
        {
            MyAccountModel myAccountModel = new MyAccountModel();
            myAccountModel = _employeeDbContext.MyAccount.Where(x => x.Email == myAccount.Email).FirstOrDefault();
            if (myAccountModel != null)
            {

                //if (myAccountModel.Email == myAccount.Email && DecryptPassword(myAccountModel.Password) == myAccount.Password)
                if (myAccountModel.Email == myAccount.Email && myAccountModel.Password == myAccount.Password)
                {
                    HttpContext.Session.SetString("Id", myAccountModel.Id.ToString());
                    HttpContext.Session.SetString("Name", myAccountModel.Name);
                    return RedirectToAction("MyAccountIndex");
                }

            }
            else
            {

            }
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        //--Password Encrypt and decrypt --

        //public static string EncryptPassword(string password)
        //{
        //    if (string.IsNullOrEmpty(password))
        //    {
        //        return null;

        //    }
        //    else
        //    {
        //        byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(password);
        //        string encryptPassword = Convert.ToBase64String(storePassword);
        //        return encryptPassword;
        //    }
        //}

        //public static string DecryptPassword(string password)
        //{
        //    if (string.IsNullOrEmpty(password))
        //    {
        //        return null;

        //    }
        //    else
        //    {
        //        byte[] encryptPassword = Convert.FromBase64String(password);
        //        string decryptPassword   = ASCIIEncoding.ASCII.GetString(encryptPassword);  
        //        return decryptPassword;
        //    }
        //}
    }
}
