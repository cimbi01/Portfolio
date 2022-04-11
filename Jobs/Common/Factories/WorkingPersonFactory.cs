using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Common.Factories
{
    public class WorkingPersonFactory : Factory
    {
        public OfferHandler OfferHandler { get; }
        public WorkingPersonFactory() :  base()
        {}
        public WorkingPersonFactory(OfferHandler offerHandler) : base()
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
    }
}
