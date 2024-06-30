using System.Net;

namespace EmployeeCrud.Models
{
    public class EmployeeModel
    {
        public int  ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateOnly Date { get; set; }
        public  string Status { get; set; }
       
    }
}
