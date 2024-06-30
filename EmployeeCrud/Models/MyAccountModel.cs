using System.ComponentModel.DataAnnotations;

namespace EmployeeCrud.Models
{
    public class MyAccountModel
    {
       
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
       
        public long Phone { get; set; }
        
        public string Password { get; set; }
            
    }
}
