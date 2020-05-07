using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class HomeDetailsViewModel
    {
        public Employee employee { get; set; }
        public Employee Employee { get; internal set; }
        public string PageTitle { get; set; }
    }
}
