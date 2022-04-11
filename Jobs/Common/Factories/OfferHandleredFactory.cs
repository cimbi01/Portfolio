using Jobs.Data;
using Jobs.Data.WorkingPerson;
using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Common.Factories
{
    public class OfferHandleredFactory : Factory
    {
        public OfferHandler OfferHandler { get; }
        public OfferHandleredFactory(OfferHandler offerHandler) : base()
        {
            this.OfferHandler = offerHandler;
        }

        /// <summary>
        /// Creates employee and adds it to <see cref="OfferHandler.Employees"/>
        /// </summary>
        /// <param name="name">Name of the employee</param>
        /// <returns></returns>
        public Employee CreateEmployee(string name)
        {
            Employee employee = new Employee(name);
            this.OfferHandler.Employees.Add(employee);
            return employee;
        }

        /// <summary>
        /// Creates employee and adds it to <see cref="OfferHandler.Employers"/>
        /// </summary>
        /// <param name="name">Name of the employer</param>
        /// <returns></returns>
        public Employer CreateEmployer(string name)
        {
            Employer employer = new Employer(name);
            this.OfferHandler.Employers.Add(employer);
            return employer;
        }

        //TODO: implementation, add joboffer to list
        public JobOffer CreateJobOffer(OfferType offerType, WorkingPerson offerer, JobData jobData, WorkingPerson? receiver = null)
        {
            throw new NotImplementedException();
        }
    }
}
