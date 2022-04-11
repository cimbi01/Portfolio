using Jobs.Data;
using Jobs.Data.WorkingPerson;
using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Common.Factories
{
    public class Factory
    {
        public OfferHandler OfferHandler { get; }
        public Factory(OfferHandler offerHandler)
        {
            this.OfferHandler = offerHandler;
        }

        public Employee CreateEmployee(string name)
        {
            Employee employee = new Employee(name);
            this.OfferHandler.Employees.Add(employee);
            return employee;
        }
       
        public Employer CreateEmployer(string name)
        {
            Employer employer = new Employer(name);
            this.OfferHandler.Employers.Add(employer);
            return employer;
        }

        //TODO: implementation
        public Skill CreateSkill()
        {
            throw new NotImplementedException();
        }

        public Reference CreateReference(string name, string url, string? details = null)
        {
            Uri urlGenerated = new Uri(url);
            Reference reference = new Reference(name, urlGenerated, details);
            return reference;
        }

        //TODO: implementation
        public Contact CreateContact()
        {
            throw new NotImplementedException();
        }

        //TODO: implementation
        public ProfessionData CreateProfessionData()
        {
            throw new NotImplementedException();
        }
    }
}
