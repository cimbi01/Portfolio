using Jobs.Common;
using Jobs.Data.WorkingPerson;
using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {

        }
        public Employee? CreatedEmployee { get; set; }
        public Employer? CreatedEmployer { get; set; }
        public string? SelectedUserName { get; set; }
    }
}
