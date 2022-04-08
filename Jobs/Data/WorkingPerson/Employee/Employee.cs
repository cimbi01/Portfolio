using System;
using System.Collections.Generic;

namespace Jobs.Data.WorkingPerson.Employee
{
    public class Employee : WorkingPerson
    {
        public ProfessionData ProfessionData { get; set; }

        // TODO: Businness Logic -> separate class -> Singleton
        public void ApplyForJob(JobData jobData)
        {
            //TODO: implementation
            throw new NotImplementedException();
        }
    }
}