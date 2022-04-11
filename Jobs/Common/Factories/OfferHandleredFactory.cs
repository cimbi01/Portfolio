using Jobs.Data;
using Jobs.Data.WorkingPerson;
using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using Jobs.Exceptions;
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

        /// <summary>
        /// Creates joboffer and adds it to <see cref="OfferHandler.JobOffers"/>
        /// </summary>
        /// <exception cref="NotAuthorizedException"/>
        /// <exception cref="EmployerNotMatchException"/>
        public JobOffer CreateJobOffer(OfferType offerType, JobData jobData, WorkingPerson offerer,  WorkingPerson? receiver = null)
        {
            if(receiver == null)
            {
                if(offerType != OfferType.Advertisement)
                {
                    throw new NotAuthorizedException();
                }    
            }

            if (offerer is Employee)
            {

                if (receiver is Employee)
                {
                    throw new NotAuthorizedException();
                }

                if (receiver is Employer && receiver != jobData.Employer)
                {
                    throw new EmployerNotMatchException();
                }

                if (offerType == OfferType.Advertisement ||
                     offerType == OfferType.Offering)
                {
                    throw new NotAuthorizedException();
                }


            }
            //offerer is Employer
            else
            {
                if (receiver is Employer)
                {
                    throw new NotAuthorizedException();
                }

                if (offerer != jobData.Employer)
                {

                    throw new EmployerNotMatchException();
                }

                if (offerType == OfferType.Application)
                {
                    throw new NotAuthorizedException();
                }
            }
            JobOffer jobOffer = new JobOffer(offerType, jobData, offerer, receiver);
            this.OfferHandler.JobOffers.Add(jobOffer);
            return jobOffer;
        }
    }
}
