using Jobs.Common;
using Jobs.Common.Factories;
using Jobs.Data.WorkingPerson;
using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class UserHandlerService
    {
        /// <summary>
        /// Currently active (selected) user. Can be null of no ActiveUser is selected
        /// </summary>
        public WorkingPerson? ActiveUser { get; set; }
        public bool ActiveUserIsEmployee()
        {
            return this.ActiveUser is Employee;
        }

        public bool ActiveUserIsEmployer()
        {
            return this.ActiveUser is Employer;
        }

        public UserHandlerService()
        {}
    }
}
