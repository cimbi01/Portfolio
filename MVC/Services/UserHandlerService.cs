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
        public List<WorkingPerson> WorkingPeople
        {
            get
            {
                List<WorkingPerson> workingPeople = new List<WorkingPerson>();
                this.OfferHandler.Employees.ForEach(emp => workingPeople.Add(emp));
                this.OfferHandler.Employers.ForEach(emp => workingPeople.Add(emp));
                return workingPeople;
            }
        }
        /// <summary>
        /// Currently active (selected) user
        /// </summary>
        public WorkingPerson ActiveUser { get; set; }
        public bool ActiveUserIsEmployee()
        {
            return this.ActiveUser is Employee;
        }

        public bool ActiveUserIsEmployer()
        {
            return this.ActiveUser is Employer;
        }

        public UserHandlerService(OfferHandler offerHandler)
        {
            this.OfferHandler = offerHandler;
        }

        public OfferHandler OfferHandler { get; }
        
        public void CreateUser(string name, bool employee)
        {
            OfferHandleredFactory factory = new OfferHandleredFactory(this.OfferHandler);
            if(employee)
            {
                factory.CreateEmployee(name);
            }
            else
            {
                factory.CreateEmployer(name);
            }
        }
    }
}
