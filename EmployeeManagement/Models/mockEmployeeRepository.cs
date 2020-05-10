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
                new Employee(){Id=1,Name="Rajesh",Email="raj@gmail.com",Department=Dept.HR},
                new Employee(){Id=2,Name="Mani",Email="mani@gmail.com",Department=Dept.IT},
                new Employee(){Id=3,Name="Mahinder",Email="Mahinder@gmail.com",Department=Dept.Payroll}
            };
        }

        public Employee Add(Employee employee)
        {
           employee.Id= _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
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
