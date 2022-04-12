using Jobs.Common;
using Jobs.Common.Factories;
using Jobs.Data.WorkingPerson;
using Jobs.Data.WorkingPerson.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class UserHandlerService
    {
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
