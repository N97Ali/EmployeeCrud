using EmployeeCrud.Models;
using Microsoft.EntityFrameworkCore;
namespace EmployeeCrud.ApplicationDbContext

{
    public class EmployeeDbContext: DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> option ):base(option) { }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<MyAccountModel> MyAccount { get; set; }
        public DbSet<StudentModel> Students { get; set; }

    }
}
