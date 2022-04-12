using Jobs.Data;
using Jobs.Data.WorkingPerson.Employer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Common.Factories
{
    public class SeededOfferHandleredFactory : OfferHandleredFactory
    {
        private const int NUMBEROFEMPLOYEES = 5;
        private const int NUMBEROFEMPLOYERS = 5;

        private static bool seeded = false;

        public SeededOfferHandleredFactory(OfferHandler offerHandler) : base(offerHandler)
        {
            lock(this)
            { 
                if(!seeded)
                {
                    this.SeedData();
                    seeded = true;
                }
            }
        }
        public void SeedData()
        {
            for (int i = 0; i < NUMBEROFEMPLOYEES; i++)
            {
                string name = "Employee" + Convert.ToString(i);
                this.CreateEmployee(name);
            }

            for (int i = 0; i < NUMBEROFEMPLOYERS; i++)
            {
                string name = "Employer" + Convert.ToString(i);
                this.CreateEmployer(name);
            }

            List<Employer> employers = this.OfferHandler.Employers;
            Employer employer = employers[0];

            for (int i = 0; i < length; i++)
            {
                JobData jobdata1 = this.CreateJobData("jobData1", employer);
                jobdata1.Details = "Details1";
                //TODO: NeededSkills, HadSkill -> List -> Dictionary
                jobdata1.NeededSkills.AddRange(
                    new List<Skill>()
                    {
                    this.CreateSkill("A", 3),
                    this.CreateSkill("B", 4),
                    this.CreateSkill("C", 2),
                    this.CreateSkill("D", 1),
                    });
                this.OfferHandler.AdvertiseJob(jobdata1, employer);
            }
        }
    }
}
