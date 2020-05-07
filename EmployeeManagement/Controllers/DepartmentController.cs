using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class DepartmentController : Controller
    {
       public string List()
        {
            return "List() of DepartmentController";
        }
        public string Details()
        {
            return "Details() of DepartmentController";
        }
    }
}