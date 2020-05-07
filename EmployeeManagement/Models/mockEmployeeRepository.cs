using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class mockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public mockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee(){Id=1,Name="Rajesh",Email="raj@gmail.com",Department="IT"},
                new Employee(){Id=2,Name="Mani",Email="mani@gmail.com",Department="Chemical"},
                new Employee(){Id=3,Name="Mahinder",Email="Mahinder@gmail.com",Department="IT Services"}
            };
        }

        public IEnumerable<Employee> GetAllEmloyee()
        {

            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }
    }
}
